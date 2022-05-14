using AutoFixture;
using AutoMapper;
using CloudFileSystem.Application.UseCases.ReadDocumentProperties;
using CloudFileSystem.Common.Builders.RequestBuilders;
using CloudFileSystem.Common.Setups;
using CloudFileSystem.Common.Verifications;
using CloudFileSystem.Domain.Abstractions;
using CloudFileSystem.Domain.Exceptions;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace CloudFileSystem.Application.UnitTests.UseCases;

internal class ReadDocumentPropertiesUseCaseTests
{
    private Mock<ICloudFileSystemDbContext> _dbContext;
    private IFixture _fixture;
    private Mock<ILogger<ReadDocumentPropertiesUseCase>> _logger;
    private Mock<IMapper> _mapper;
    private ReadDocumentPropertiesUseCase _useCase;

    [Test]
    public async Task Handle_ReadDocumentPropertiesRequest_Should_ReturnDocumentProperties()
    {
        var documentId = _fixture.Create<Guid>();
        _dbContext.ContainsDocument(documentId);
        _mapper.BuildResponseBasedOnDocument(documentId);
        var request = ReadDocumentPropertiesRequestBuilder.ReadDocumentPropertiesRequest()
            .DocumentId(documentId)
            .Build();

        var actual = await _useCase.Handle(request);

        _dbContext.HasRetrievedDocument(documentId);
        _mapper.HasBuiltResponse();
        actual.Should().BeOfType<ReadDocumentPropertiesResponse>()
            .Which.Id.Should().Be(documentId);
    }

    [Test]
    public async Task Handle_ReadDocumentPropertiesRequest_Should_ThrowMissingDocumentException_When_DocumentDoesNotExist()
    {
        var request = ReadDocumentPropertiesRequestBuilder.ReadDocumentPropertiesRequest().Build();

        Func<Task> act = () => _useCase.Handle(request);

        (await act.Should().ThrowAsync<NotFoundException>())
            .Which.Id.Should().Be(request.Id.Value);
    }

    [Test]
    public void ReadDocumentPropertiesUseCase_Should_ThrowArgumentNullException_When_LoggerIsNull()
    {
        Action act = () => new ReadDocumentPropertiesUseCase(null, _dbContext.Object, _mapper.Object);

        act.Should().Throw<ArgumentNullException>()
            .WithParameterName("logger");
    }

    [Test]
    public void ReadDocumentPropertiesUseCase_Should_ThrowArgumentNullException_When_MapperIsNull()
    {
        Action act = () => new ReadDocumentPropertiesUseCase(_logger.Object, _dbContext.Object, null);

        act.Should().Throw<ArgumentNullException>()
            .WithParameterName("mapper");
    }

    [SetUp]
    public void SetUp()
    {
        _fixture = new Fixture();
        _dbContext = new Mock<ICloudFileSystemDbContext>();
        _logger = new Mock<ILogger<ReadDocumentPropertiesUseCase>>();
        _mapper = new Mock<IMapper>();
        _useCase = new ReadDocumentPropertiesUseCase(_logger.Object, _dbContext.Object, _mapper.Object); ;
    }
}