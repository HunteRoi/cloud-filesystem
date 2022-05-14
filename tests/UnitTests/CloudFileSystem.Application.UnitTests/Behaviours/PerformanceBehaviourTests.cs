using CloudFileSystem.Application.Behaviours;
using FluentAssertions;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace CloudFileSystem.Application.UnitTests.Behaviours;

internal class PerformanceBehaviourTests
{
    private Mock<ILogger<FakeRequest>> _logger;
    private PerformanceBehaviour<FakeRequest, FakeResponse> _behaviour;

    [SetUp]
    public void SetUp()
    {
        _logger = new Mock<ILogger<FakeRequest>>();
        _behaviour = new PerformanceBehaviour<FakeRequest, FakeResponse>(_logger.Object);
    }

    [Test]
    public void PerformanceBehaviour_Should_ThrowArgumentNullException_When_LoggerIsNull()
    {
        Assert.Throws<ArgumentNullException>(() => new PerformanceBehaviour<FakeRequest, FakeResponse>(null));
    }

    [Test]
    public async Task PerformanceBehaviour_Should_ReturnResponse()
    {
        var request = new FakeRequest();
        var response = new FakeResponse(request.Id);
        Mock<RequestHandlerDelegate<FakeResponse>> act = new();
        act.Setup(callback => callback.Invoke()).ReturnsAsync(response).Verifiable();

        var actual = await _behaviour.Handle(request, default, act.Object);

        act.Verify(callback => callback.Invoke(), Times.Once);
        actual.Should().BeEquivalentTo(response);
    }
}