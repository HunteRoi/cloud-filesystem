using CloudFileSystem.Domain.Abstractions;
using Moq;
using System;

namespace CloudFileSystem.WebAPI.UnitTests.Common.Setups;

internal static class ExceptionSerializerSetups
{
    public static string ShouldSerialize(this Mock<IExceptionSerializer> exceptionSerializer, Exception exception)
    {
        string exceptionSerialized = exception.ToString();
        exceptionSerializer.Setup(serializer => serializer.SerializeException(exception)).Returns(exceptionSerialized).Verifiable();
        return exceptionSerialized;
    }
}