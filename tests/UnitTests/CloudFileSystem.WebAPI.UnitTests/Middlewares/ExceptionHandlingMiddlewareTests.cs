using AutoFixture;
using AutoFixture.Kernel;
using CloudFileSystem.Application.Exceptions;
using CloudFileSystem.Domain.Abstractions;
using CloudFileSystem.WebAPI.Middlewares.ExceptionHandling;
using CloudFileSystem.WebAPI.UnitTests.Common.Setups;
using CloudFileSystem.WebAPI.UnitTests.Common.Verifications;
using FluentAssertions;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.IO;
using System.Threading.Tasks;

namespace CloudFileSystem.WebAPI.UnitTests.Middlewares
{
    internal class ExceptionHandlingMiddlewareTests
    {
        private Mock<IExceptionSerializer> _exceptionSerializer;
        private IFixture _fixture;
        private Mock<ILogger<ExceptionHandlingMiddleware>> _logger;
        private ExceptionHandlingMiddleware _middleware;

        private static DefaultHttpContext CreateHttpContext(string path = null, string queryString = null, string requestBody = null)
        {
            DefaultHttpContext context = new();
            MemoryStream bodyStream = new();

            if (!string.IsNullOrWhiteSpace(requestBody))
            {
                using (var stream = new MemoryStream())
                using (var writer = new StreamWriter(stream))
                {
                    writer.Write(requestBody);
                    writer.Flush();
                    stream.Seek(0, SeekOrigin.Begin);
                    stream.CopyTo(bodyStream);
                }
            }

            context.Request.Path = path;
            context.Request.QueryString = string.IsNullOrWhiteSpace(queryString) ? QueryString.Empty : new QueryString(queryString);
            context.Request.Body = bodyStream;
            context.Response.Body = new MemoryStream();

            return context;
        }

        [Test]
        public void ExceptionHandlingMiddleware_Should_ThrowArgumentNullException_When_ExceptionSerializerIsNull()
        {
            Action act = () => new ExceptionHandlingMiddleware(_logger.Object, null);

            act.Should().Throw<ArgumentNullException>()
                .WithParameterName("exceptionSerializer");
        }

        [Test]
        public void ExceptionHandlingMiddleware_Should_ThrowArgumentNullException_When_LoggerIsNull()
        {
            Action act = () => new ExceptionHandlingMiddleware(null, _exceptionSerializer.Object);

            act.Should().Throw<ArgumentNullException>()
                .WithParameterName("logger");
        }

        [Test]
        [TestCase(typeof(ValidationException))]
        [TestCase(typeof(BadRequestException))]
        [TestCase(typeof(NotFoundException))]
        [TestCase(typeof(Exception))]
        public async Task InvokeAsync_Should_WriteErrorResponseAsJson(Type exceptionType)
        {
            var context = CreateHttpContext();
            var exception = new SpecimenContext(_fixture).Resolve(exceptionType) as Exception;
            var serializedException = _exceptionSerializer.ShouldSerialize(exception);

            await _middleware.InvokeAsync(context, (HttpContext context) => throw exception);

            _exceptionSerializer.HasSerialized(exception);
            context.Response.ShouldTransport(serializedException, Error.From(exception).StatusCode);
        }

        [SetUp]
        public void SetUp()
        {
            _fixture = new Fixture();
            _logger = new Mock<ILogger<ExceptionHandlingMiddleware>>();
            _exceptionSerializer = new Mock<IExceptionSerializer>();
            _middleware = new ExceptionHandlingMiddleware(_logger.Object, _exceptionSerializer.Object);
        }
    }
}