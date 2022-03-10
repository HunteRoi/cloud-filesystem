using CloudFileSystem.Core.V1.FileManagement.Archive;
using CloudFileSystem.Core.V1.FileManagement.Storage;
using CloudFileSystem.Domain.V1.Extensions;
using CloudFileSystem.Domain.V1.Interfaces.Data;
using CloudFileSystem.Infrastructure.V1.Archive;
using CloudFileSystem.Infrastructure.V1.Data.Contexts;
using CloudFileSystem.Infrastructure.V1.Data.Interfaces;
using CloudFileSystem.Infrastructure.V1.Data.Repositories;
using CloudFileSystem.Infrastructure.V1.Storage.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace CloudFileSystem.Infrastructure.V1.Extensions;

/// <summary>
/// Extensions of <see cref="IServiceCollection" />
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds the infrastructure layer.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <returns></returns>
    public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services)
    {
        services.AddDomainLayer();

        services.TryAddScoped(provider =>
        {
            IConfiguration configuration = provider.GetService<IConfiguration>();
            return new AzureStorageOptions(configuration["Azure:AccountStorage:ConnectionString"], configuration["Azure:AccountStorage:FileShareName"]);
        });
        services.TryAddScoped<IStorageManager, AzureFileShareStorageManager>();
        services.TryAddScoped<IArchiveManager, SharpZipLibArchiveManager>();

        services
            .AddDbContext<CloudFileSystemDbContext>()
            .TryAddScoped<ICloudFileSystemDataContext, CloudFileSystemDbContext>();
        services.TryAddScoped<ICloudFileSystemRepository, CloudFileSystemRepository>();

        return services;
    }
}