namespace CloudFileSystem.Core.V1.Exceptions;

/// <summary>
/// Thrown when an entity could not be updated
/// </summary>
/// <typeparam name="T"></typeparam>
/// <seealso cref="System.Exception" />
public class UpdateException<T> : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateException{T}" /> class.
    /// </summary>
    /// <param name="message">The message.</param>
    /// <param name="inner">The inner.</param>
    public UpdateException(string message, Exception inner)
        : base(message, inner)
    {
    }
}