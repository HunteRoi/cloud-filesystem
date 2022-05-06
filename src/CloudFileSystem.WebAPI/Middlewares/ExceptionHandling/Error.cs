using CloudFileSystem.Application.Exceptions;
using FluentValidation;

namespace CloudFileSystem.WebAPI.Middlewares.ExceptionHandling;

public record Error(int StatusCode, string Title)
{
    public static Error BadRequest => new(StatusCodes.Status400BadRequest, "Bad Request");
    public static Error NotFound => new(StatusCodes.Status404NotFound, "Not Found");
    public static Error Validation => new(StatusCodes.Status422UnprocessableEntity, "Validation Exception");
    public static Error Unknown => new(StatusCodes.Status500InternalServerError, "Unknown Error");

    public static Error From(Exception exception)
        => exception switch
        {
            BadRequestException => BadRequest,
            NotFoundException => NotFound,
            ValidationException => Validation,
            _ => Unknown
        };
}