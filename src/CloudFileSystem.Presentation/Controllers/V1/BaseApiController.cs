using CloudFileSystem.Core.V1.Responses;
using Microsoft.AspNetCore.Mvc;

namespace CloudFileSystem.API.Controllers.V1;

/// <summary>
/// Base API Controller class
/// </summary>
/// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
[Consumes("application/json")]
[Produces("application/json")]
public class BaseApiController : Controller
{
    /// <summary>
    /// Gets the action result.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <param name="response">The response.</param>
    /// <returns></returns>
    protected IActionResult GetActionResult(Guid id, object response)
    {
        return response == null ? this.GetNotFoundActionResult(id) : this.GetOkActionResult(response);
    }

    /// <summary>
    /// Gets the bad request action result.
    /// </summary>
    /// <returns></returns>
    protected IActionResult GetBadRequestActionResult()
    {
        return this.BadRequest(new ApiResponse<IEnumerable<string>>(null, false)); // create a service to handle errors throughout the application and return them here
    }

    /// <summary>
    /// Gets the error action result.
    /// </summary>
    /// <param name="exception">The exception.</param>
    /// <returns></returns>
    protected IActionResult GetErrorActionResult(Exception exception)
    {
        return this.StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<string>(exception.Message, false));
    }

    /// <summary>
    /// Gets the file result.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <param name="response">The response.</param>
    /// <returns></returns>
    protected IActionResult GetFileResult(Guid id, FileResponse response) => response == null
        ? this.GetNotFoundActionResult(id)
        : this.File(response.Content, response.ContentType, response.Name);

    /// <summary>
    /// Gets the file result.
    /// </summary>
    /// <param name="ids">The ids.</param>
    /// <param name="response">The response.</param>
    /// <returns></returns>
    protected IActionResult GetFileResult(IEnumerable<Guid> ids, FileResponse response) => response == null
        ? this.GetNotFoundActionResult(ids)
        : this.File(response.Content, response.ContentType, response.Name);

    /// <summary>
    /// Gets the content of the no.
    /// </summary>
    /// <returns></returns>
    protected IActionResult GetNoContentResult()
    {
        return this.NoContent();
    }

    /// <summary>
    /// Gets the not found action result.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <returns></returns>
    protected IActionResult GetNotFoundActionResult(Guid id) => this.NotFound(new ApiResponse<Guid>(id, false));

    /// <summary>
    /// Gets the not found action result.
    /// </summary>
    /// <param name="ids">The ids.</param>
    /// <returns></returns>
    protected IActionResult GetNotFoundActionResult(IEnumerable<Guid> ids) => this.NotFound(new ApiResponse<IEnumerable<Guid>>(ids, false));

    /// <summary>
    /// Gets the ok action result.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="response">The response.</param>
    /// <returns></returns>
    protected IActionResult GetOkActionResult<T>(T response) where T : class
    {
        return this.Ok(new ApiResponse<T>(response));
    }
}