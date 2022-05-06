using CloudFileSystem.Domain.Abstractions;
using FluentValidation;
using System.Text.Json;

namespace CloudFileSystem.Infrastructure;

internal sealed record ErrorResponse(string Details, IDictionary<string, string[]>? Errors);

public class ExceptionSerializer : IExceptionSerializer
{
    private static IDictionary<string, string[]>? GetErrors(Exception exception)
    {
        if (exception is ValidationException validationException)
        {
            return validationException.Errors
                .GroupBy(x => x.PropertyName, x => x.ErrorMessage, (propertyName, errorMessages) => new
                {
                    Key = propertyName,
                    Values = errorMessages.Distinct().ToArray()
                })
                .ToDictionary(x => x.Key, x => x.Values);
        }

        return null;
    }

    public string SerializeException(Exception exception)
    {
        ErrorResponse response = new(exception.Message, GetErrors(exception));
        return JsonSerializer.Serialize(response);
    }
}