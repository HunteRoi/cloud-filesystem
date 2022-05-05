namespace CloudFileSystem.Application.Exceptions;

public class MissingDocumentException : Exception
{
    public MissingDocumentException(Guid documentId)
        : base()
    {
        DocumentId = documentId;
    }

    public Guid DocumentId { get; }
}