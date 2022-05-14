using MediatR;

namespace CloudFileSystem.Application.UseCases.ReadDocumentProperties;

public sealed record ReadDocumentPropertiesRequest(Guid? Id) : IRequest<ReadDocumentPropertiesResponse>;