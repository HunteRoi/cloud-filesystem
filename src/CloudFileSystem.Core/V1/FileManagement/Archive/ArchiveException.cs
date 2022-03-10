using System.Runtime.Serialization;

namespace CloudFileSystem.Core.V1.FileManagement.Archive;

/// <summary>
/// Thrown when an error occurs while using the storage manager
/// </summary>
/// <seealso cref="System.Exception" />
[Serializable]
public class ArchiveException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ArchiveException" /> class.
    /// </summary>
    /// <param name="info">
    /// The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the
    /// serialized object data about the exception being thrown.
    /// </param>
    /// <param name="context">
    /// The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual
    /// information about the source or destination.
    /// </param>
    protected ArchiveException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ArchiveException" /> class.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public ArchiveException(string message) : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ArchiveException" /> class.
    /// </summary>
    /// <param name="propertyName">The v1.</param>
    /// <param name="methodName">The v2.</param>
    public ArchiveException(string propertyName, string methodName)
        : this($"{propertyName} {methodName}")
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ArchiveException" /> class.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="innerException">
    /// The exception that is the cause of the current exception, or a null reference ( <see
    /// langword="Nothing" /> in Visual Basic) if no inner exception is specified.
    /// </param>
    public ArchiveException(string message, Exception innerException) : base(message, innerException)
    {
    }
}