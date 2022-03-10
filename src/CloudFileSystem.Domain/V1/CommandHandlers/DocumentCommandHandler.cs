using CloudFileSystem.Core.V1.FileManagement.Storage;
using CloudFileSystem.Domain.V1.Aggregates.DocumentAggregate;
using CloudFileSystem.Domain.V1.Commands.Document;
using CloudFileSystem.Domain.V1.Interfaces.Data;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CloudFileSystem.Domain.V1.CommandHandlers;

/// <summary>
/// The commands handler of the Document entity
/// </summary>
/// <seealso cref="MediatR.IRequestHandler{CloudFileSystem.Domain.V1.Commands.Document.CreateDocumentCommand, System.Guid}" />
/// <seealso cref="MediatR.IRequestHandler{CloudFileSystem.Domain.V1.Commands.Document.UpdateDocumentCommand, System.Guid}" />
/// <seealso cref="MediatR.IRequestHandler{CloudFileSystem.Domain.V1.Commands.Document.DeleteDocumentCommand}" />
public class DocumentCommandHandler : IRequestHandler<CreateDocumentCommand, Guid>,
                                      IRequestHandler<UpdateDocumentCommand, Guid>,
                                      IRequestHandler<DeleteDocumentCommand>
{
    /// <summary>
    /// The logger
    /// </summary>
    private readonly ILogger<DocumentCommandHandler> _logger;

    /// <summary>
    /// The repository
    /// </summary>
    private readonly ICloudFileSystemRepository _repository;

    /// <summary>
    /// The storage manager
    /// </summary>
    private readonly IStorageManager _storageManager;

    /// <summary>
    /// Initializes a new instance of the <see cref="DocumentCommandHandler" /> class.
    /// </summary>
    /// <param name="logger">The logger.</param>
    /// <param name="repository">The repository.</param>
    /// <param name="storageManager">The storage manager.</param>
    /// <exception cref="System.ArgumentNullException">logger or repository or storageManager</exception>
    public DocumentCommandHandler(ILogger<DocumentCommandHandler> logger, ICloudFileSystemRepository repository, IStorageManager storageManager)
    {
        this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
        this._repository = repository ?? throw new ArgumentNullException(nameof(repository));
        this._storageManager = storageManager ?? throw new ArgumentNullException(nameof(storageManager));
    }

    /// <summary>
    /// Handles a create request of a Document record.
    /// </summary>
    /// <param name="request">The request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Response from the request</returns>
    public async Task<Guid> Handle(CreateDocumentCommand request, CancellationToken cancellationToken)
    {
        try
        {
            Document document = request.IsFolder
                ? new Document(request.Name, request.ParentId)
                : new Document(request.Name, request.Extension, request.ParentId);
            Guid id = await this._repository.CreateDocumentAsync(document);

            if (!request.IsFolder)
            {
                request.File.SafeName = id.ToString();
                await this._storageManager.UploadAsync(request.File);
            }

            return id;
        }
        catch (Exception exception)
        {
            this._logger.LogError(exception, exception.Message, request);
            return Guid.Empty;
        }
    }

    /// <summary>
    /// Handles an update request on a Document record.
    /// </summary>
    /// <param name="request">The request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Response from the request</returns>
    /// <exception cref="System.ArgumentNullException">document</exception>
    /// <exception cref="System.ArgumentException">
    /// You cannot convert a file to a folder nor a folder to a file!
    /// </exception>
    public async Task<Guid> Handle(UpdateDocumentCommand request, CancellationToken cancellationToken)
    {
        try
        {
            Document document = await this._repository.GetDocumentAsync(request.Id);
            if (document is null) throw new ArgumentNullException(nameof(document));

            if (document.IsFolder && !request.IsFolder || !document.IsFolder && request.IsFolder)
            {
                throw new ArgumentException("You cannot convert a file to a folder nor a folder to a file!");
            }

            document.Rename(request.Name, request.Extension);
            document.MoveTo(request.ParentId);

            if (!request.IsFolder)
            {
                request.File.SafeName = document.Id.ToString();
                await this._storageManager.UploadAsync(request.File, true);
            }

            await this._repository.UpdateDocumentAsync(document);
            return document.Id;
        }
        catch (Exception exception)
        {
            this._logger.LogError(exception, exception.Message, request);
            return Guid.Empty;
        }
    }

    /// <summary>
    /// Handles a deletion request of a Document record.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    public async Task<Unit> Handle(DeleteDocumentCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await this._repository.DeleteDocumentAsync(request.Id);
        }
        catch (Exception exception)
        {
            this._logger.LogError(exception, exception.Message, request);
        }
        return Unit.Value;
    }
}