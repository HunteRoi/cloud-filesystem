using AutoMapper;
using CloudFileSystem.Application.V1.Requests.Document;
using CloudFileSystem.Application.V1.Responses.Document;
using CloudFileSystem.Core.V1.FileManagement;
using CloudFileSystem.Core.V1.Responses;
using CloudFileSystem.Domain.V1.Aggregates.DocumentAggregate;
using CloudFileSystem.Domain.V1.Commands.Document;
using CloudFileSystem.Domain.V1.Queries.Document;

namespace CloudFileSystem.Application.V1.Mappings;

/// <summary>
/// Mapping profile class
/// </summary>
/// <seealso cref="AutoMapper.Profile" />
public class MappingProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MappingProfile" /> class.
    /// </summary>
    public MappingProfile()
    {
        this.CreateQueriesMapping();
        this.CreateCommandsMapping();
        this.CreateResponsesMapping();
    }

    /// <summary>
    /// Creates the commands mapping.
    /// </summary>
    protected void CreateCommandsMapping()
    {
        this.CreateMap<CreateDocumentRequest, CreateDocumentCommand>()
            .ConvertUsing(request => new CreateDocumentCommand(request.Name, request.ParentId, request.File));
        this.CreateMap<UpdateDocumentRequest, UpdateDocumentCommand>()
            .ConvertUsing(request => new UpdateDocumentCommand(request.Id, request.Name, request.ParentId, request.File));
    }

    /// <summary>
    /// Creates the queries mapping.
    /// </summary>
    protected void CreateQueriesMapping()
    {
        this.CreateMap<DownloadDocumentsRequest, DownloadDocumentsQuery>();
        this.CreateMap<GetDocumentsRequest, GetDocumentsQuery>()
            .ConvertUsing(request => new GetDocumentsQuery(request.ParentId));
    }

    /// <summary>
    /// Creates the responses mapping.
    /// </summary>
    protected void CreateResponsesMapping()
    {
        this.CreateMap<Document, DocumentResponse>()
            .ForMember(dr => dr.LastModifiedAt, opts => opts.MapFrom(d => d.LastModifiedAt ?? d.CreatedAt))
            .ForMember(dr => dr.LastModifiedBy, opts => opts.MapFrom(d => d.LastModifiedBy ?? d.CreatedBy));
        this.CreateMap<StorageFile, FileResponse>();
    }
}