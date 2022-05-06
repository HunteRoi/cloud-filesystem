using AutoMapper;
using CloudFileSystem.Application.UseCases.ReadDocumentProperties;
using CloudFileSystem.Common.Builders.ResponseBuilders;
using CloudFileSystem.Domain.Entities;
using Moq;

namespace CloudFileSystem.Common.Setups;

public static class MapperSetup
{
    public static void BuildResponseBasedOnDocument(this Mock<IMapper> mapper, Guid documentId)
    {
        var response = ReadDocumentPropertiesResponseBuilder.ReadDocumentPropertiesResponse()
            .DocumentId(documentId)
            .Build();

        mapper.Setup(mapper => mapper.Map<ReadDocumentPropertiesResponse>(It.IsNotNull<Document>()))
            .Returns(response)
            .Verifiable();
    }
}