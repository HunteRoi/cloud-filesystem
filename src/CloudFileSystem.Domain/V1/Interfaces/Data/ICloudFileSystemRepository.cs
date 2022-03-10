using CloudFileSystem.Domain.V1.Aggregates.DocumentAggregate;

namespace CloudFileSystem.Domain.V1.Interfaces.Data;

/// <summary>
/// The repository interface
/// </summary>
public interface ICloudFileSystemRepository
{
    /// <summary>
    /// Creates the document asynchronous.
    /// </summary>
    /// <param name="document">The document.</param>
    /// <returns></returns>
    Task<Guid> CreateDocumentAsync(Document document);

    /// <summary>
    /// Deletes the document asynchronous.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <returns></returns>
    Task DeleteDocumentAsync(Guid id);

    /// <summary>
    /// Gets the document asynchronous.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <returns></returns>
    Task<Document> GetDocumentAsync(Guid id);

    /// <summary>
    /// Gets the documents asynchronous.
    /// </summary>
    /// <param name="parentId">The parent identifier.</param>
    /// <returns></returns>
    Task<IEnumerable<Document>> GetDocumentsAsync(Guid? parentId = null);

    /// <summary>
    /// Gets the documents asynchronous.
    /// </summary>
    /// <param name="ids">The ids.</param>
    /// <returns></returns>
    Task<IEnumerable<Document>> GetDocumentsAsync(IEnumerable<Guid> ids);

    /// <summary>
    /// Updates the document asynchronous.
    /// </summary>
    /// <param name="document">The document.</param>
    /// <returns></returns>
    Task<Guid> UpdateDocumentAsync(Document document);
}