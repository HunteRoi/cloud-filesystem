using MediatR;

namespace CloudFileSystem.Application.Abstractions;

internal interface IVoidUseCase<in TRequest> : IRequestHandler<TRequest>
    where TRequest : IRequest
{
}