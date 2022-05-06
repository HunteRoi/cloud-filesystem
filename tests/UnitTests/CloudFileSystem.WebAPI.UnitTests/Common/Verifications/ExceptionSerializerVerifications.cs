using CloudFileSystem.Domain.Abstractions;
using Moq;
using System;

namespace CloudFileSystem.WebAPI.UnitTests.Common.Verifications;

internal static class ExceptionSerializerVerifications
{
    public static void HasSerialized(this Mock<IExceptionSerializer> exceptionSerializer, Exception exception)
    {
        exceptionSerializer.Verify(serializer => serializer.SerializeException(exception), Times.Once);
    }
}