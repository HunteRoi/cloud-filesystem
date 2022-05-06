using CloudFileSystem.Domain.Abstractions;
using Dawn;
using FluentValidation;

namespace CloudFileSystem.WebAPI.Middlewares.ExceptionHandling;

public sealed class ExceptionHandlingMiddleware : IMiddleware
{
    private readonly IExceptionSerializer _exceptionSerializer;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger, IExceptionSerializer exceptionSerializer)
    {
        _logger = Guard.Argument(logger, nameof(logger)).NotNull().Value;
        _exceptionSerializer = Guard.Argument(exceptionSerializer, nameof(exceptionSerializer)).NotNull().Value;
    }

    private async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
    {
        var error = Error.From(exception);

        httpContext.Response.StatusCode = error.StatusCode;
        var json = _exceptionSerializer.SerializeException(exception);

        await httpContext.Response.WriteAsync(json);
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