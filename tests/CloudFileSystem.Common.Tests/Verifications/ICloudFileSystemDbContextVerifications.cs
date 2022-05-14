using CloudFileSystem.Domain.Abstractions;
using CloudFileSystem.Domain.Entities;
using Moq;

namespace CloudFileSystem.Common.Verifications;

public static class ICloudFileSystemDbContextVerifications
{
    public static void HasRetrievedDocument(this Mock<ICloudFileSystemDbContext> repository, Guid documentId)
    {
        repository.Verify(repo => repo.GetEntityAsync<Document>(documentId), Times.Once);
    }
}