using AutoMapper;
using CloudFileSystem.Application.UseCases.ReadDocumentProperties;
using CloudFileSystem.Domain.Entities;

namespace CloudFileSystem.Application.Mappings;

public class DocumentProfile : Profile
{
    public DocumentProfile()
    {
        CreateMap<Document, ReadDocumentPropertiesResponse>();
    }
}