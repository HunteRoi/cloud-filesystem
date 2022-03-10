using CloudFileSystem.Application.V1.Requests.Document;
using CloudFileSystem.Application.V1.Responses.Document;
using CloudFileSystem.Core.V1.Responses;

namespace CloudFileSystem.Application.V1.Interfaces;

/// <summary>
/// The application service interface.
/// </summary>
public interface IAppService
{
    /// <summary>
    /// Creates the document asynchronous.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns></returns>
    Task<Guid> CreateDocumentAsync(CreateDocumentRequest request);

    /// <summary>
    /// Deletes the document asynchronous.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <returns></returns>
    Task DeleteDocumentAsync(Guid id);

    /// <summary>
    /// Downloads the document asynchronous.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <returns></returns>
    Task<FileResponse> DownloadDocumentAsync(Guid id);

    /// <summary>
    /// Downloads the documents asynchronous.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns></returns>
    Task<FileResponse> DownloadDocumentsAsync(DownloadDocumentsRequest request);

    /// <summary>
    /// Gets the document asynchronous.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <returns></returns>
    Task<DocumentResponse> GetDocumentAsync(Guid id);

    /// <summary>
    /// Gets the documents asynchronous.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns></returns>
    Task<IEnumerable<DocumentResponse>> GetDocumentsAsync(GetDocumentsRequest request);

    /// <summary>
    /// Updates the document asynchronous.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns></returns>
    Task<Guid> UpdateDocumentAsync(UpdateDocumentRequest request);
}