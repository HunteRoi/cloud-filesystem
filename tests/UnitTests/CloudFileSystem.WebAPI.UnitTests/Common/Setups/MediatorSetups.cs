using CloudFileSystem.Application.UseCases.ReadDocumentProperties;
using CloudFileSystem.Common.Builders.ResponseBuilders;
using MediatR;
using Moq;
using System;
using System.Threading;

namespace CloudFileSystem.WebAPI.UnitTests.Common.Setups;

internal static class MediatorSetups
{
    public static ReadDocumentPropertiesResponse ReturnsPropertiesForDocument(this Mock<IMediator> mediator, Guid documentId)
    {
        var response = ReadDocumentPropertiesResponseBuilder.ReadDocumentPropertiesResponse().DocumentId(documentId).Build();

        mediator
            .Setup(mediator => mediator.Send(It.Is<ReadDocumentPropertiesRequest>(r => r.Id.Value == documentId), It.IsAny<CancellationToken>()))
            .ReturnsAsync(response)
            .Verifiable();

        return response;
    }

    public static void ReturnsNullForDocument(this Mock<IMediator> mediator, Guid documentId)
    {
        mediator
            .Setup(mediator => mediator.Send(It.Is<ReadDocumentPropertiesRequest>(r => r.Id.Value == documentId), It.IsAny<CancellationToken>()))
            .ReturnsAsync(() => null)
            .Verifiable();
    }
}