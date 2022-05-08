using CloudFileSystem.Application.Behaviours;
using NUnit.Framework;
using System;

namespace CloudFileSystem.Application.UnitTests.Behaviours;

internal class PerformanceBehaviourTests
{
    [Test]
    public void PerformanceBehaviour_Should_ThrowArgumentNullException_When_LoggerIsNull()
    {
        Assert.Throws<ArgumentNullException>(() => new PerformanceBehaviour<FakeRequest, FakeResponse>(null));
    }
}