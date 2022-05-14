using CloudFileSystem.Application.UseCases.ReadDocumentProperties;
using CloudFileSystem.Domain.Exceptions;
using Dawn;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CloudFileSystem.WebAPI.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/[controller]")]
public class DocumentsController : ControllerBase
{
    private readonly IMediator _mediator;

    public DocumentsController(IMediator mediator)
    {
        _mediator = Guard.Argument(mediator, nameof(mediator)).NotNull().Value;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> ReadDocumentProperties(Guid? id)
    {
        var request = new ReadDocumentPropertiesRequest(id);
        var response = await _mediator.Send(request);
        if (response == null)
        {
            throw new NotFoundException(id.Value);
        }

        return Ok(response);
    }
}