using CloudFileSystem.Domain.Entities;

namespace CloudFileSystem.Common.Builders.Entities;

public class DocumentBuilder
{
    private const string DefaultDocumentId = "5f9a9d5f-94aa-453a-8139-3b5c0c8c25a0";
    private Guid _documentId;

    public DocumentBuilder()
    {
        DocumentId(Guid.Parse(DefaultDocumentId));
    }

    public static DocumentBuilder Document()
    {
        return new DocumentBuilder();
    }

    public Document Build()
    {
        return new Document()
        {
            DocumentId = _documentId
        };
    }

    public DocumentBuilder DocumentId(Guid documentId)
    {
        _documentId = documentId;
        return this;
    }
}