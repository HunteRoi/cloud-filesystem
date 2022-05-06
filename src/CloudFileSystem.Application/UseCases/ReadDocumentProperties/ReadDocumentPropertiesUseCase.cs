using AutoMapper;
using CloudFileSystem.Application.Abstractions;
using CloudFileSystem.Domain.Exceptions;
using Dawn;
using Microsoft.Extensions.Logging;

namespace CloudFileSystem.Application.UseCases.ReadDocumentProperties;

public class ReadDocumentPropertiesUseCase : IUseCase<ReadDocumentPropertiesRequest, ReadDocumentPropertiesResponse>
{
    private readonly IDocumentRepository _documentRepository;
    private readonly ILogger<ReadDocumentPropertiesUseCase> _logger;
    private readonly IMapper _mapper;

    public ReadDocumentPropertiesUseCase(ILogger<ReadDocumentPropertiesUseCase> logger, IDocumentRepository documentRepository, IMapper mapper)
    {
        _logger = Guard.Argument(logger, nameof(logger)).NotNull().Value;
        _documentRepository = Guard.Argument(documentRepository, nameof(documentRepository)).NotNull().Value;
        _mapper = Guard.Argument(mapper, nameof(mapper)).NotNull().Value;
    }

    public async Task<ReadDocumentPropertiesResponse> Handle(ReadDocumentPropertiesRequest request, CancellationToken cancellationToken = default)
    {
        var document = await _documentRepository.GetByDocumentId(request.DocumentId);
        if (document == null)
        {
            throw new NotFoundException(request.DocumentId);
        }

        return _mapper.Map<ReadDocumentPropertiesResponse>(document);
    }
}