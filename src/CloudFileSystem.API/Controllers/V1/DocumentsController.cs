
using CloudFileSystem.Application.V1.Interfaces;
using CloudFileSystem.Application.V1.Requests.Document;
using CloudFileSystem.Application.V1.Responses.Document;
using CloudFileSystem.Core.V1.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace CloudFileSystem.API.Controllers.V1
{
    /// <summary>
    /// The endpoint to create, read, update or delete a Document entity
    /// </summary>
    /// <seealso cref="CloudFileSystem.API.Controllers.V1.BaseApiController" />
    [Route("api/v{version:ApiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class DocumentsController : BaseApiController
    {
        /// <summary>
        /// The request size limit
        /// </summary>
        private const int _REQUEST_SIZE_LIMIT = 60000000;

        /// <summary>
        /// The application service
        /// </summary>
        private readonly IAppService _appService;

        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger<DocumentsController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentsController"/> class.
        /// </summary>
        /// <param name="appService">The application service.</param>
        /// <param name="logger">The logger.</param>
        /// <exception cref="System.ArgumentNullException">
        /// appService
        /// or
        /// logger
        /// </exception>
        public DocumentsController(IAppService appService, ILogger<DocumentsController> logger)
            : base()
        {
            this._appService = appService ?? throw new ArgumentNullException(nameof(appService));
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Gets the documents asynchronous.
        /// </summary>
        /// <param name="parentId">The parent identifier.</param>
        /// <returns></returns>
        [HttpGet]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<DocumentResponse>>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Description = "Unexpected error", Type = typeof(ApiResponse<string>))]
        public async Task<IActionResult> GetDocumentsAsync([FromQuery] Guid? parentId = null)
        {
            try
            {
                var request = new GetDocumentsRequest(parentId);
                IEnumerable<DocumentResponse> documents = await this._appService.GetDocumentsAsync(request);
                return this.GetOkActionResult(documents);
            }
            catch (Exception exception)
            {
                this._logger.LogError(exception, exception.Message, parentId);
                return this.GetErrorActionResult(exception);
            }
        }

        /// <summary>
        /// Gets the document by identifier asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ApiResponse<DocumentResponse>))]
        [SwaggerResponse((int)HttpStatusCode.NotFound, Type = typeof(ApiResponse<Guid>))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(ApiResponse<IEnumerable<string>>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Description = "Unexpected error", Type = typeof(ApiResponse<string>))]
        public async Task<IActionResult> GetDocumentByIdAsync([FromRoute] Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                {
                    return this.GetBadRequestActionResult();
                }

                DocumentResponse document = await this._appService.GetDocumentAsync(id);
                return this.GetActionResult(id, document);
            }
            catch (Exception exception)
            {
                this._logger.LogError(exception, exception.Message, id);
                return this.GetErrorActionResult(exception);
            }
        }

        /// <summary>
        /// Downloads the document by identifier asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet("{id}/download")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(FileStreamResult))]
        [SwaggerResponse((int)HttpStatusCode.NotFound, Type = typeof(ApiResponse<Guid>))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(ApiResponse<IEnumerable<string>>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Description = "Unexpected error", Type = typeof(ApiResponse<string>))]
        public async Task<IActionResult> DownloadDocumentByIdAsync([FromRoute] Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                {
                    return this.GetBadRequestActionResult();
                }

                FileResponse file = await this._appService.DownloadDocumentAsync(id);
                return this.GetFileResult(id, file);
            }
            catch (Exception exception)
            {
                this._logger.LogError(exception, exception.Message, id);
                return this.GetErrorActionResult(exception);
            }
        }

        /// <summary>
        /// Downloads the documents by identifier asynchronous.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [HttpGet("download")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(FileStreamResult))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(ApiResponse<IEnumerable<string>>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Description = "Unexpected error", Type = typeof(ApiResponse<string>))]
        public async Task<IActionResult> DownloadDocumentsByIdAsync([FromBody] DownloadDocumentsRequest request)
        {
            try
            {
                FileResponse response = await this._appService.DownloadDocumentsAsync(request);
                return this.GetFileResult(request.Ids, response);
            }
            catch (Exception exception)
            {
                this._logger.LogError(exception, exception.Message, request);
                return this.GetErrorActionResult(exception);
            }
        }


        /// <summary>
        /// Creates the document asynchronous.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [HttpPost]
        [RequestSizeLimit(DocumentsController._REQUEST_SIZE_LIMIT)]
        [RequestFormLimits(MultipartBodyLengthLimit = DocumentsController._REQUEST_SIZE_LIMIT, ValueLengthLimit = DocumentsController._REQUEST_SIZE_LIMIT)]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(FileStreamResult))]
        [SwaggerResponse((int)HttpStatusCode.NotFound, Type = typeof(ApiResponse<Guid>))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(ApiResponse<IEnumerable<string>>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Description = "Unexpected error", Type = typeof(ApiResponse<string>))]
        public async Task<IActionResult> CreateDocumentAsync([FromForm] CreateDocumentRequest request)
        {
            try
            {
                Guid id = await this._appService.CreateDocumentAsync(request);
                if (id == Guid.Empty)
                {
                    return this.GetBadRequestActionResult();
                }
                DocumentResponse document = await this._appService.GetDocumentAsync(id);
                return this.GetActionResult(id, document);
            }
            catch (Exception exception)
            {
                this._logger.LogError(exception, exception.Message, request);
                return this.GetErrorActionResult(exception);
            }
        }

        /// <summary>
        /// Updates the document asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [RequestSizeLimit(DocumentsController._REQUEST_SIZE_LIMIT)]
        [RequestFormLimits(MultipartBodyLengthLimit = DocumentsController._REQUEST_SIZE_LIMIT, ValueLengthLimit = DocumentsController._REQUEST_SIZE_LIMIT)]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(FileStreamResult))]
        [SwaggerResponse((int)HttpStatusCode.NotFound, Type = typeof(ApiResponse<Guid>))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(ApiResponse<IEnumerable<string>>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Description = "Unexpected error", Type = typeof(ApiResponse<string>))]
        public async Task<IActionResult> UpdateDocumentAsync([FromRoute] Guid id, [FromForm] UpdateDocumentRequest request)
        {
            try
            {
                if (id == Guid.Empty)
                {
                    return this.GetBadRequestActionResult();
                }

                request.SetEntityId(id);
                await this._appService.UpdateDocumentAsync(request);

                DocumentResponse document = await this._appService.GetDocumentAsync(id);
                return this.GetActionResult(id, document);
            }
            catch (Exception exception)
            {
                this._logger.LogError(exception, exception.Message, request);
                return this.GetErrorActionResult(exception);
            }
        }

        /// <summary>
        /// Deletes the document asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [SwaggerResponse((int)HttpStatusCode.NoContent)]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(ApiResponse<IEnumerable<string>>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Description = "Unexpected error", Type = typeof(ApiResponse<string>))]
        public async Task<IActionResult> DeleteDocumentAsync([FromRoute] Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                {
                    return this.GetBadRequestActionResult();
                }

                await this._appService.DeleteDocumentAsync(id);
                return this.GetNoContentResult();
            }
            catch (Exception exception)
            {
                this._logger.LogError(exception, exception.Message, id);
                return this.GetErrorActionResult(exception);
            }
        }
    }
}
