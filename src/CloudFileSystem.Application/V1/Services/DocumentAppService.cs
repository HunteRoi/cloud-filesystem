using CloudFileSystem.Application.V1.Requests.Document;
using CloudFileSystem.Application.V1.Responses.Document;
using CloudFileSystem.Core.V1.FileManagement;
using CloudFileSystem.Core.V1.Responses;
using CloudFileSystem.Domain.V1.Aggregates.DocumentAggregate;
using CloudFileSystem.Domain.V1.Commands.Document;
using CloudFileSystem.Domain.V1.Queries.Document;
using Microsoft.Extensions.Logging;

namespace CloudFileSystem.Application.V1.Services;

/// <summary>
/// The partial class of the application service specifically handling actions for the documents.
/// </summary>
/// <seealso cref="CloudFileSystem.Application.V1.Interfaces.IAppService" />
public partial class AppService
{
    /// <inheritdoc />
    public Task<Guid> CreateDocumentAsync(CreateDocumentRequest request)
    {
        this._logger.LogDebug(nameof(this.CreateDocumentAsync), request);
        var command = this._mapper.Map<CreateDocumentCommand>(request);
        return this._mediator.Send(command);
    }

    /// <inheritdoc />
    public Task DeleteDocumentAsync(Guid id)
    {
        this._logger.LogDebug(nameof(this.DeleteDocumentAsync), id);
        var command = new DeleteDocumentCommand(id);
        return this._mediator.Send(command);
    }

    /// <inheritdoc />
    public async Task<FileResponse> DownloadDocumentAsync(Guid id)
    {
        this._logger.LogDebug(nameof(this.DownloadDocumentAsync), id);
        var query = new DownloadDocumentQuery(id);
        StorageFile response = await this._mediator.Send(query);
        return this._mapper.Map<FileResponse>(response);
    }

    /// <inheritdoc />
    public async Task<FileResponse> DownloadDocumentsAsync(DownloadDocumentsRequest request)
    {
        this._logger.LogDebug(nameof(this.DownloadDocumentsAsync), request);
        var query = this._mapper.Map<DownloadDocumentsQuery>(request);
        StorageFile response = await this._mediator.Send(query);
        return this._mapper.Map<FileResponse>(response);
    }

    /// <inheritdoc />
    public async Task<DocumentResponse> GetDocumentAsync(Guid id)
    {
        this._logger.LogDebug(nameof(this.GetDocumentAsync), id);
        var query = new GetDocumentQuery(id);
        Document document = await this._mediator.Send(query);
        return this._mapper.Map<DocumentResponse>(document);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<DocumentResponse>> GetDocumentsAsync(GetDocumentsRequest request)
    {
        this._logger.LogDebug(nameof(this.GetDocumentsAsync), request);
        var query = this._mapper.Map<GetDocumentsQuery>(request);
        IEnumerable<Document> documents = await this._mediator.Send(query);
        return this._mapper.Map<IEnumerable<DocumentResponse>>(documents);
    }

    /// <inheritdoc />
    public Task<Guid> UpdateDocumentAsync(UpdateDocumentRequest request)
    {
        this._logger.LogDebug(nameof(this.UpdateDocumentAsync), request);
        var command = this._mapper.Map<UpdateDocumentCommand>(request);
        return this._mediator.Send(command);
    }
}