using CloudFileSystem.Core.V1.FileManagement;
using CloudFileSystem.Domain.V1.Aggregates.DocumentAggregate;
using CloudFileSystem.Domain.V1.CommandHandlers;
using CloudFileSystem.Domain.V1.Commands.Document;
using CloudFileSystem.Domain.V1.Queries.Document;
using CloudFileSystem.Domain.V1.QueryHandlers;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace CloudFileSystem.Domain.V1.Extensions;

/// <summary>
/// Extensions of <see cref="IServiceCollection" />
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds the domain layer.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <returns></returns>
    public static IServiceCollection AddDomainLayer(this IServiceCollection services)
    {
        services.TryAddScoped<IRequestHandler<CreateDocumentCommand, Guid>, DocumentCommandHandler>();
        services.TryAddScoped<IRequestHandler<UpdateDocumentCommand, Guid>, DocumentCommandHandler>();
        services.TryAddScoped<IRequestHandler<DeleteDocumentCommand, Unit>, DocumentCommandHandler>();

        services.TryAddScoped<IRequestHandler<GetDocumentQuery, Document>, DocumentQueryHandler>();
        services.TryAddScoped<IRequestHandler<GetDocumentsQuery, IEnumerable<Document>>, DocumentQueryHandler>();
        services.TryAddScoped<IRequestHandler<DownloadDocumentQuery, StorageFile>, DocumentQueryHandler>();
        services.TryAddScoped<IRequestHandler<DownloadDocumentsQuery, StorageFile>, DocumentQueryHandler>();

        return services;
    }
}