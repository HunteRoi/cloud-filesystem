namespace CloudFileSystem.Application.Exceptions;

public class MissingDocumentException : NotFoundException
{
    public MissingDocumentException(Guid documentId)
        : base()
    {
        DocumentId = documentId;
    }

    public Guid DocumentId { get; }
}