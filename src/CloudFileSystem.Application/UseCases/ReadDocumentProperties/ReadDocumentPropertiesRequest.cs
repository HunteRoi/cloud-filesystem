using MediatR;

namespace CloudFileSystem.Application.UseCases.ReadDocumentProperties;

public sealed record ReadDocumentPropertiesRequest(Guid DocumentId) : IRequest<ReadDocumentPropertiesResponse>;