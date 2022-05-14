using AutoMapper;
using CloudFileSystem.Application.Abstractions;
using CloudFileSystem.Domain.Abstractions;
using CloudFileSystem.Domain.Entities;
using CloudFileSystem.Domain.Exceptions;
using Dawn;
using Microsoft.Extensions.Logging;

namespace CloudFileSystem.Application.UseCases.ReadDocumentProperties;

public class ReadDocumentPropertiesUseCase : IUseCase<ReadDocumentPropertiesRequest, ReadDocumentPropertiesResponse>
{
    private readonly ICloudFileSystemDbContext _dbContext;
    private readonly ILogger<ReadDocumentPropertiesUseCase> _logger;
    private readonly IMapper _mapper;

    public ReadDocumentPropertiesUseCase(ILogger<ReadDocumentPropertiesUseCase> logger, ICloudFileSystemDbContext dbContext, IMapper mapper)
    {
        _logger = Guard.Argument(logger, nameof(logger)).NotNull().Value;
        _dbContext = Guard.Argument(dbContext, nameof(dbContext)).NotNull().Value;
        _mapper = Guard.Argument(mapper, nameof(mapper)).NotNull().Value;
    }

    public async Task<ReadDocumentPropertiesResponse> Handle(ReadDocumentPropertiesRequest request, CancellationToken cancellationToken = default)
    {
        var document = await _dbContext.GetEntityAsync<Document>(request.Id.Value);
        if (document == null)
        {
            throw new NotFoundException(request.Id.Value);
        }

        return _mapper.Map<ReadDocumentPropertiesResponse>(document);
    }
}