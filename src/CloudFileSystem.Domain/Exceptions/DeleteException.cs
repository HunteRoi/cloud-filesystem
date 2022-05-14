namespace CloudFileSystem.Domain.Exceptions;

/// <summary>
/// Thrown when an entity could not be deleted
/// </summary>
/// <typeparam name="T"></typeparam>
/// <seealso cref="System.Exception"/>
public class DeleteException<T> : ApplicationException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteException{T}"/> class.
    /// </summary>
    /// <param name="message">The message.</param>
    /// <param name="inner">The inner.</param>
    public DeleteException(string message, Exception inner)
        : base(message, inner)
    {
    }
}