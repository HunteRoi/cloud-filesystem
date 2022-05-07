using FluentAssertions;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace CloudFileSystem.WebAPI.UnitTests.Common.Verifications;

internal static class HttpResponseVerifications
{
    public static void ShouldTransport(this HttpResponse response, string json, int statusCode)
    {
        response.StatusCode.Should().Be(statusCode);

        response.Body.Position = 0;
        using var reader = new StreamReader(response.Body);
        reader.ReadToEnd().Should().Be(json);
    }
}