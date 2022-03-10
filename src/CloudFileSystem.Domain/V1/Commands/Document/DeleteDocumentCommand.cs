using MediatR;

namespace CloudFileSystem.Domain.V1.Commands.Document;

/// <summary>
/// Command to delete a document
/// </summary>
/// <seealso cref="MediatR.IRequest" />
public class DeleteDocumentCommand : IRequest
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteDocumentCommand" /> class.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <exception cref="System.ArgumentException">id</exception>
    public DeleteDocumentCommand(Guid id)
    {
        this.Id = id != Guid.Empty ? id : throw new ArgumentException(nameof(id));
    }

    /// <summary>
    /// Gets the identifier.
    /// </summary>
    /// <value>The identifier.</value>
    public Guid Id { get; }
}