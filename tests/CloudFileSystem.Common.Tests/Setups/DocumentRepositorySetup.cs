using CloudFileSystem.Application.Abstractions;
using CloudFileSystem.Common.Builders.Entities;
using Moq;

namespace CloudFileSystem.Common.Setups;

public static class DocumentRepositorySetup
{
    public static void ContainsDocument(this Mock<IDocumentRepository> repository, Guid documentId)
    {
        var document = DocumentBuilder.Document()
            .DocumentId(documentId)
            .Build();

        repository.Setup(repo => repo.GetByDocumentId(documentId))
            .ReturnsAsync(document)
            .Verifiable();
    }
}