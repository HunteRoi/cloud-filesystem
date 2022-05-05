using MediatR;

namespace CloudFileSystem.Application.Abstractions;

internal interface IUseCase<in TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
}