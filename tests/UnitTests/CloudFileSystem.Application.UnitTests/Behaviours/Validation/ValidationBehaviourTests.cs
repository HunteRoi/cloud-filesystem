using CloudFileSystem.Application.Behaviours;
using FluentAssertions;
using FluentValidation;
using MediatR;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace CloudFileSystem.Application.UnitTests.Behaviours.Validation;

internal class ValidationBehaviourTests
{
    [Test]
    public void ValidationBehaviour_Should_ThrowArgumentNullException_When_ListOfValidatorsIsNull()
    {
        Assert.Throws<ArgumentNullException>(() => new ValidationBehaviour<FakeRequest, FakeResponse>(null));
    }

    [Test]
    public void ValidationBehaviour_Should_ThrowValidationException_When_RequestHasEmptyId()
    {
        var request = new FakeRequest();
        IEnumerable<IValidator<FakeRequest>> validators = new List<IValidator<FakeRequest>> { new FakeRequestValidator() };
        var behaviour = new ValidationBehaviour<FakeRequest, FakeResponse>(validators);
        var expectedErrors = new Dictionary<string, string[]>()
        {
            { "Id", new[] { "'Id' must not be empty." } }
        };

        var exception = Assert.ThrowsAsync<Domain.Exceptions.ValidationException>(() => behaviour.Handle(request, default, () => null));
        exception.Errors.Should().BeEquivalentTo(expectedErrors);
    }

    [Test]
    public void ValidationBehaviour_Should_CallRequestHandler_When_RequestHasNoValidators()
    {
        var request = new FakeRequest();
        var response = new FakeResponse(request.Id);
        IEnumerable<IValidator<FakeRequest>> validators = new List<IValidator<FakeRequest>>();
        var behaviour = new ValidationBehaviour<FakeRequest, FakeResponse>(validators);
        Mock<RequestHandlerDelegate<FakeResponse>> act = new();
        act.Setup(callback => callback.Invoke()).ReturnsAsync(response).Verifiable();

        Assert.DoesNotThrowAsync(() => behaviour.Handle(request, default, act.Object));
        act.Verify(callback => callback.Invoke(), Times.Once);
        validators.Should().BeEmpty();
    }

    [Test]
    public void ValidationBehaviour_Should_CallRequestHandler_When_RequestPassedValidators()
    {
        var request = new FakeRequest(Guid.NewGuid());
        var response = new FakeResponse(request.Id);
        IEnumerable<IValidator<FakeRequest>> validators = new List<IValidator<FakeRequest>> { new FakeRequestValidator() };
        var behaviour = new ValidationBehaviour<FakeRequest, FakeResponse>(validators);
        Mock<RequestHandlerDelegate<FakeResponse>> act = new();
        act.Setup(callback => callback.Invoke()).ReturnsAsync(response).Verifiable();

        Assert.DoesNotThrowAsync(() => behaviour.Handle(request, default, act.Object));
        act.Verify(callback => callback.Invoke(), Times.Once);
        validators.Should().NotBeEmpty();
    }
}