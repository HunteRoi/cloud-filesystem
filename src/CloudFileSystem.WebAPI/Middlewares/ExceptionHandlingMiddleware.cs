using CloudFileSystem.Application.Exceptions;
using Dawn;
using FluentValidation;
using FluentValidation.Results;
using System.Text.Json;

namespace CloudFileSystem.WebAPI.Middlewares;

internal sealed class ExceptionHandlingMiddleware : IMiddleware
{
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger)
    {
        _logger = Guard.Argument(logger, nameof(logger)).NotNull().Value;
    }

    private static IEnumerable<ValidationFailure>? GetErrors(Exception exception)
    {
        if (exception is ValidationException validationException)
        {
            return validationException.Errors;
        }
        return null;
    }

    private static int GetStatusCode(Exception exception) =>
        exception switch
        {
            BadRequestException => StatusCodes.Status400BadRequest,
            NotFoundException => StatusCodes.Status404NotFound,
            ValidationException => StatusCodes.Status422UnprocessableEntity,
            _ => StatusCodes.Status500InternalServerError
        };

    private static string GetTitle(Exception exception) =>
        exception switch
        {
            BadRequestException => "Bad Request",
            NotFoundException => "Not Found",
            ValidationException => "Validation Error",
            _ => "Unknown Error"
        };

    private static async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
    {
        var statusCode = GetStatusCode(exception);
        var response = new
        {
            title = GetTitle(exception),
            status = statusCode,
            detail = exception.Message,
            errors = GetErrors(exception)
        };
        httpContext.Response.ContentType = "application/json";
        httpContext.Response.StatusCode = statusCode;
        await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response));
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, exception.Message);
            await HandleExceptionAsync(context, exception);
        }
    }
}