using CloudFileSystem.Domain.V1.Aggregates.DocumentAggregate;

namespace CloudFileSystem.Infrastructure.V1.Data.Repositories;

/// <summary>
/// Repository class related to operations on the Document entity.
/// </summary>
/// <seealso cref="CloudFileSystem.Infrastructure.V1.Data.Repositories.Repository" />
/// <seealso cref="CloudFileSystem.Domain.V1.Interfaces.Data.ICloudFileSystemRepository" />
public partial class CloudFileSystemRepository
{
    /// <inheritdoc />
    public Task<Guid> CreateDocumentAsync(Document document)
    {
        return base.CreateEntityAsync<Document>(document);
    }

    /// <inheritdoc />
    public Task DeleteDocumentAsync(Guid id)
    {
        return base.DeleteEntityAsync<Document>(id);
    }

    /// <inheritdoc />
    public Task<Document> GetDocumentAsync(Guid id)
    {
        return base.GetEntityAsync<Document>(id);
    }

    /// <inheritdoc />
    public Task<IEnumerable<Document>> GetDocumentsAsync(Guid? parentId = null)
    {
        return base.GetEntitiesAsync<Document>(d => d.ParentId == parentId);
    }

    /// <inheritdoc />
    public Task<IEnumerable<Document>> GetDocumentsAsync(IEnumerable<Guid> ids)
    {
        return base.GetEntitiesAsync<Document>(d => ids.Contains(d.Id));
    }

    /// <inheritdoc />
    public Task<Guid> UpdateDocumentAsync(Document document)
    {
        return base.UpdateEntityAsync<Document>(document);
    }
}