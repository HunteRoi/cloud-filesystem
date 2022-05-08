using CloudFileSystem.Application.Behaviours;
using FluentAssertions;
using FluentValidation;
using MediatR;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace CloudFileSystem.Application.UnitTests.Behaviours;

internal class ValidationBehaviourTests
{
    private IEnumerable<IValidator<FakeRequest>> _validators;
    private ValidationBehaviour<FakeRequest, FakeResponse> _behaviour;

    [SetUp]
    public void SetUp()
    {
        _validators = new List<IValidator<FakeRequest>> { new FakeRequestValidator() };
        _behaviour = new ValidationBehaviour<FakeRequest, FakeResponse>(_validators);
    }

    [Test]
    public void ValidationBehaviour_Should_ThrowArgumentNullException_When_ListOfValidatorsIsNull()
    {
        Assert.Throws<ArgumentNullException>(() => new ValidationBehaviour<FakeRequest, FakeResponse>(null));
    }

    [Test]
    public void Handle_Should_ThrowValidationException_When_RequestHasEmptyId()
    {
        var request = new FakeRequest();
        var expectedErrors = new Dictionary<string, string[]>()
        {
            { "Id", new[] { "'Id' must not be empty." } }
        };

        var exception = Assert.ThrowsAsync<Domain.Exceptions.ValidationException>(() => _behaviour.Handle(request, default, () => null));
        exception.Errors.Should().BeEquivalentTo(expectedErrors);
    }

    [Test]
    public void Handle_Should_CallRequestHandler_When_RequestHasNoValidators()
    {
        var request = new FakeRequest();
        var response = new FakeResponse(request.Id);
        _validators = new List<IValidator<FakeRequest>>();
        Mock<RequestHandlerDelegate<FakeResponse>> act = new();
        act.Setup(callback => callback.Invoke()).ReturnsAsync(response).Verifiable();

        Assert.DoesNotThrowAsync(() => _behaviour.Handle(request, default, act.Object));
        act.Verify(callback => callback.Invoke(), Times.Once);
        _validators.Should().BeEmpty();
    }

    [Test]
    public void Handle_Should_CallRequestHandler_When_RequestPassedValidators()
    {
        var request = new FakeRequest(Guid.NewGuid());
        var response = new FakeResponse(request.Id);
        Mock<RequestHandlerDelegate<FakeResponse>> act = new();
        act.Setup(callback => callback.Invoke()).ReturnsAsync(response).Verifiable();

        Assert.DoesNotThrowAsync(() => _behaviour.Handle(request, default, act.Object));
        act.Verify(callback => callback.Invoke(), Times.Once);
        _validators.Should().NotBeEmpty();
    }
}