using CloudFileSystem.Application.Abstractions;
using Moq;

namespace CloudFileSystem.Common.Verifications;

public static class DocumentRepositoryVerifications
{
    public static void HasRetrievedDocument(this Mock<IDocumentRepository> repository, Guid documentId)
    {
        repository.Verify(repo => repo.GetByDocumentId(documentId), Times.Once);
    }
}