namespace CloudFileSystem.Core.V1.Exceptions;

/// <summary>
/// Thrown when an entity could not be created
/// </summary>
/// <typeparam name="T"></typeparam>
/// <seealso cref="System.Exception" />
public class CreateException<T> : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateException{T}" /> class.
    /// </summary>
    /// <param name="message">The message.</param>
    /// <param name="inner">The inner.</param>
    public CreateException(string message, Exception inner)
        : base(message, inner)
    {
    }
}