using CloudFileSystem.Core.V1.FileManagement;
using CloudFileSystem.Core.V1.FileManagement.Archive;
using CloudFileSystem.Core.V1.FileManagement.Storage;
using CloudFileSystem.Domain.V1.Aggregates.DocumentAggregate;
using CloudFileSystem.Domain.V1.Interfaces.Data;
using CloudFileSystem.Domain.V1.Queries.Document;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CloudFileSystem.Domain.V1.QueryHandlers;

/// <summary>
/// The query handler for the Document records
/// </summary>
/// <seealso cref="MediatR.IRequestHandler{CloudFileSystem.Domain.V1.Queries.Document.GetDocumentQuery, CloudFileSystem.Domain.V1.Aggregates.DocumentAggregate.Document}" />
/// <seealso cref="MediatR.IRequestHandler{CloudFileSystem.Domain.V1.Queries.Document.GetDocumentsQuery, System.Collections.Generic.IEnumerable{CloudFileSystem.Domain.V1.Aggregates.DocumentAggregate.Document}}" />
/// <seealso cref="MediatR.IRequestHandler{CloudFileSystem.Domain.V1.Queries.Document.DownloadDocumentQuery, CloudFileSystem.Core.V1.FileManagement.StorageFile}" />
/// <seealso cref="MediatR.IRequestHandler{CloudFileSystem.Domain.V1.Queries.Document.DownloadDocumentsQuery, CloudFileSystem.Core.V1.FileManagement.StorageFile}" />
public class DocumentQueryHandler : IRequestHandler<GetDocumentQuery, Document>,
                                    IRequestHandler<GetDocumentsQuery, IEnumerable<Document>>,
                                    IRequestHandler<DownloadDocumentQuery, StorageFile>,
                                    IRequestHandler<DownloadDocumentsQuery, StorageFile>
{
    /// <summary>
    /// The maximum simultaneous download
    /// </summary>
    private const int MAX_SIMULTANEOUS_DOWNLOAD = 10;

    /// <summary>
    /// The archive manager
    /// </summary>
    private readonly IArchiveManager _archiveManager;

    /// <summary>
    /// The logger
    /// </summary>
    private readonly ILogger<DocumentQueryHandler> _logger;

    /// <summary>
    /// The repository
    /// </summary>
    private readonly ICloudFileSystemRepository _repository;

    /// <summary>
    /// The storage manager
    /// </summary>
    private readonly IStorageManager _storageManager;

    /// <summary>
    /// Initializes a new instance of the <see cref="DocumentQueryHandler" /> class.
    /// </summary>
    /// <param name="logger">The logger.</param>
    /// <param name="repository">The repository.</param>
    /// <param name="storageManager">The storage manager.</param>
    /// <param name="archiveManager">The archive manager.</param>
    /// <exception cref="System.ArgumentNullException">
    /// logger or repository or storageManager or archiveManager
    /// </exception>
    public DocumentQueryHandler(ILogger<DocumentQueryHandler> logger, ICloudFileSystemRepository repository, IStorageManager storageManager, IArchiveManager archiveManager)
    {
        this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
        this._repository = repository ?? throw new ArgumentNullException(nameof(repository));
        this._storageManager = storageManager ?? throw new ArgumentNullException(nameof(storageManager));
        this._archiveManager = archiveManager ?? throw new ArgumentNullException(nameof(archiveManager));
    }

    /// <summary>
    /// Downloads the asynchronous.
    /// </summary>
    /// <param name="documentId">The document identifier.</param>
    /// <param name="semaphore">The semaphore.</param>
    /// <returns></returns>
    private async Task<StorageFile> DownloadAsync(Guid documentId, SemaphoreSlim semaphore)
    {
        this._logger.LogDebug(nameof(this.DownloadAsync), documentId, semaphore);
        try
        {
            await semaphore.WaitAsync();

            return await this._storageManager.DownloadAsync(documentId.ToString());
        }
        catch (Exception exception)
        {
            this._logger.LogError(exception, exception.Message, nameof(this.DownloadAsync), documentId, semaphore);
            throw;
        }
        finally
        {
            semaphore.Release();
        }
    }

    /// <summary>
    /// Handles a request
    /// </summary>
    /// <param name="request">The request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Response from the request</returns>
    public Task<Document> Handle(GetDocumentQuery request, CancellationToken cancellationToken)
    {
        this._logger.LogDebug($"Handle {nameof(GetDocumentQuery)}", request);
        try
        {
            return this._repository.GetDocumentAsync(request.Id);
        }
        catch (Exception exception)
        {
            this._logger.LogError(exception, exception.Message, request);
            return null;
        }
    }

    /// <summary>
    /// Handles a request
    /// </summary>
    /// <param name="request">The request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Response from the request</returns>
    public Task<IEnumerable<Document>> Handle(GetDocumentsQuery request, CancellationToken cancellationToken)
    {
        this._logger.LogDebug($"Handle {nameof(GetDocumentsQuery)}", request);
        try
        {
            return this._repository.GetDocumentsAsync(request.ParentId);
        }
        catch (Exception exception)
        {
            this._logger.LogError(exception, exception.Message, request);
            return null;
        }
    }

    /// <summary>
    /// Handles a request
    /// </summary>
    /// <param name="request">The request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Response from the request</returns>
    /// <exception cref="System.ArgumentNullException">document</exception>
    public async Task<StorageFile> Handle(DownloadDocumentQuery request, CancellationToken cancellationToken)
    {
        this._logger.LogDebug($"Handle {nameof(DownloadDocumentQuery)}", request);
        try
        {
            Document document = await this._repository.GetDocumentAsync(request.Id);
            if (document is null) throw new ArgumentNullException(nameof(document));

            StorageFile file = await this._storageManager.DownloadAsync(document.Id.ToString());
            return file;
        }
        catch (Exception exception)
        {
            this._logger.LogError(exception, exception.Message, request);
            return null;
        }
    }

    /// <summary>
    /// Handles a request
    /// </summary>
    /// <param name="request">The request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Response from the request</returns>
    public async Task<StorageFile> Handle(DownloadDocumentsQuery request, CancellationToken cancellationToken)
    {
        this._logger.LogDebug($"Handle {nameof(DownloadDocumentsQuery)}", request);
        try
        {
            var semaphore = new SemaphoreSlim(DocumentQueryHandler.MAX_SIMULTANEOUS_DOWNLOAD);
            IEnumerable<Task<StorageFile>> filesTasks = request.Ids.Select(id => this.DownloadAsync(id, semaphore));
            IEnumerable<StorageFile> downloadedFiles = await Task.WhenAll<StorageFile>(filesTasks);
            return this._archiveManager.Compress(downloadedFiles);
        }
        catch (Exception exception)
        {
            this._logger.LogError(exception, exception.Message, request);
            return null;
        }
    }
}