using CloudFileSystem.Application.UseCases.ReadDocumentProperties;
using MediatR;
using Moq;
using System;
using System.Threading;

namespace CloudFileSystem.WebAPI.UnitTests.Common.Verifications;

internal static class MediatorVerifications
{
    public static void RequestedDocumentPropertiesForDocument(this Mock<IMediator> mediator, Guid documentId)
    {
        mediator
            .Verify(mediator => mediator.Send(It.Is<ReadDocumentPropertiesRequest>(request => request.Id.Value == documentId), It.IsAny<CancellationToken>()), Times.Once());
    }

    public static void ReturnedNullForDocument(this Mock<IMediator> mediator, Guid documentId)
    {
        mediator
            .Verify(mediator => mediator.Send(It.Is<ReadDocumentPropertiesRequest>(r => r.Id.Value == documentId), It.IsAny<CancellationToken>()), Times.Once());
    }
}