using AutoFixture;
using CloudFileSystem.Domain.Exceptions;
using CloudFileSystem.WebAPI.Controllers.V1;
using CloudFileSystem.WebAPI.UnitTests.Common.Setups;
using CloudFileSystem.WebAPI.UnitTests.Common.Verifications;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace CloudFileSystem.WebAPI.UnitTests.Controllers;

internal class DocumentsControllerTests
{
    private IFixture _fixture;

    private Mock<IMediator> _mediator;

    private DocumentsController _controller;

    [SetUp]
    public void SetUp()
    {
        _fixture = new Fixture();
        _mediator = new Mock<IMediator>();
        _controller = new DocumentsController(_mediator.Object);
    }

    [Test]
    public void DocumentsController_Should_ThrowArgumentNullExcption_When_MediatorIsNul()
    {
        Assert.Throws<ArgumentNullException>(() => new DocumentsController(null));
    }

    [Test]
    public void ReadDocumentProperties_Should_ThrowNotFoundException_When_DocumentDoesNotExist()
    {
        var documentId = _fixture.Create<Guid>();
        _mediator.ReturnsNullForDocument(documentId);

        Assert.ThrowsAsync<NotFoundException>(() => _controller.ReadDocumentProperties(documentId));
        _mediator.ReturnedNullForDocument(documentId);
    }

    [Test]
    public async Task ReadDocumentProperties_Should_ReturnDocumentProperties()
    {
        var documentId = _fixture.Create<Guid>();
        var response = _mediator.ReturnsPropertiesForDocument(documentId);
        var expected = new OkObjectResult(response);

        var actual = await _controller.ReadDocumentProperties(documentId);

        _mediator.RequestedDocumentPropertiesForDocument(documentId);
        actual.Should().BeOfType<OkObjectResult>()
            .Which.Should().BeEquivalentTo(expected);
    }
}