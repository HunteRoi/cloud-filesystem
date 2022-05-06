using AutoFixture;
using CloudFileSystem.Domain;
using CloudFileSystem.Domain.Exceptions;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Text.Json;

namespace CloudFileSystem.Infrastructure.UnitTests;

internal class ExceptionSerializerTests
{
    private ExceptionSerializer _exceptionSerializer;
    private IFixture _fixture;

    [Test]
    public void SerializeException_Should_SerializeExceptionAsJson()
    {
        var exception = _fixture.Create<Exception>();
        var expected = JsonSerializer.Serialize(new ErrorResponse(exception.Message, null));

        var actual = _exceptionSerializer.SerializeException(exception);

        actual.Should().Be(expected);
    }

    [Test]
    public void SerializeException_Should_SerializeValidationExceptionAsJson_With_ErrorsProperty()
    {
        var exception = _fixture.Create<ValidationException>();
        var expected = JsonSerializer.Serialize(new ErrorResponse(exception.Message, exception.Errors));

        var actual = _exceptionSerializer.SerializeException(exception);

        actual.Should().Be(expected);
    }

    [SetUp]
    public void SetUp()
    {
        _fixture = new Fixture();
        _exceptionSerializer = new ExceptionSerializer();
    }
}