using CloudFileSystem.Common.Builders.Entities;
using CloudFileSystem.Domain.Abstractions;
using CloudFileSystem.Domain.Entities;
using Moq;

namespace CloudFileSystem.Common.Setups;

public static class ICloudFileSystemDbContextSetup
{
    public static void ContainsDocument(this Mock<ICloudFileSystemDbContext> repository, Guid documentId)
    {
        var document = DocumentBuilder.Document()
            .DocumentId(documentId)
            .Build();

        repository.Setup(repo => repo.GetEntityAsync<Document>(documentId))
            .ReturnsAsync(document)
            .Verifiable();
    }
}