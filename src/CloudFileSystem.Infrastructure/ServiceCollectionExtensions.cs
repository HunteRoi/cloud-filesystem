using CloudFileSystem.Domain.Abstractions;
using CloudFileSystem.Infrastructure.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CloudFileSystem.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        string connectionString = BuildConnectionString(services);

        services.AddDbContext<ICloudFileSystemDbContext, CloudFileSystemDbContext>(cfg =>
        {
            cfg.UseSqlServer(connectionString);
        });
        services.AddTransient<IExceptionSerializer, ExceptionSerializer>();
        return services;
    }

    private static string BuildConnectionString(IServiceCollection services)
    {
        var config = services.BuildServiceProvider().GetRequiredService<IConfiguration>();
        SqlConnectionStringBuilder builder = new(config.GetConnectionString("Default"));
        builder.Password = config.GetValue<string>("DbPassword");
        builder.UserID = config.GetValue<string>("DbUserId");
        return builder.ToString();
    }
}