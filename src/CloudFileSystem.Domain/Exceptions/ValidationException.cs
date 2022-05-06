namespace CloudFileSystem.Domain.Exceptions;

public class ValidationException : ApplicationException
{
    public ValidationException(IDictionary<string, string[]>? errors)
        : base()
    {
        Errors = errors;
    }

    public ValidationException()
        : this(null)
    { }

    public IDictionary<string, string[]>? Errors { get; }
}