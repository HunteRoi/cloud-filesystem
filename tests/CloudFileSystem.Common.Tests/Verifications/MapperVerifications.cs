using AutoMapper;
using CloudFileSystem.Application.UseCases.ReadDocumentProperties;
using CloudFileSystem.Domain.Entities;
using Moq;

namespace CloudFileSystem.Common.Verifications;

public static class MapperVerifications
{
    public static void HasBuiltResponse(this Mock<IMapper> mapper)
    {
        mapper.Verify(mapper => mapper.Map<ReadDocumentPropertiesResponse>(It.IsNotNull<Document>()), Times.Once);
    }
}