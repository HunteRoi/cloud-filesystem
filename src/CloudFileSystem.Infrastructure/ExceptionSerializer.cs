using CloudFileSystem.Domain;
using CloudFileSystem.Domain.Abstractions;
using CloudFileSystem.Domain.Exceptions;
using System.Text.Json;

namespace CloudFileSystem.Infrastructure;

public class ExceptionSerializer : IExceptionSerializer
{
    public string SerializeException(Exception exception)
    {
        ErrorResponse response = new(exception.Message, exception is ValidationException validationException ? validationException.Errors : null);
        return JsonSerializer.Serialize(response);
    }
}