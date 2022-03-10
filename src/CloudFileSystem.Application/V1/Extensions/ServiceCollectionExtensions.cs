using CloudFileSystem.Application.V1.Configuration;
using CloudFileSystem.Application.V1.Interfaces;
using CloudFileSystem.Application.V1.Services;
using CloudFileSystem.Domain.V1.Extensions;
using CloudFileSystem.Swagger.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Newtonsoft.Json.Serialization;

namespace CloudFileSystem.Application.V1.Extensions;

/// <summary>
/// Extensions of <see cref="IServiceCollection" />
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds the application layer.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <param name="configuration">The configuration.</param>
    /// <returns></returns>
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services, ServicesConfiguration configuration)
    {
        services.TryAddScoped<IAppService, AppService>();

        services
            .AddDomainLayer()
            .AddRouting(options => options.LowercaseUrls = true)
            .AddVersioning(configuration.ApiVersion)
            .AddAutoMapper(configuration.MappingProfilesAssemblyType)
            .AddMediatR(configuration.StartupAssemblyType)
            .AddSwaggerDocumentation()
            .AddMvc()
            .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                options.SerializerSettings.DateFormatHandling = Newtonsoft.Json.DateFormatHandling.IsoDateFormat;
                options.SerializerSettings.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Utc;
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                options.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
                options.SerializerSettings.ContractResolver = new DefaultContractResolver { NamingStrategy = new CamelCaseNamingStrategy() };
            });

        services.AddControllers();

        return services;
    }

    /// <summary>
    /// Adds the versioning.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <param name="defaultApiVersion">The default API version.</param>
    /// <returns></returns>
    public static IServiceCollection AddVersioning(this IServiceCollection services, ApiVersion defaultApiVersion)
    {
        return services
            .AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = defaultApiVersion;
            })
            .AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VV";
                options.SubstituteApiVersionInUrl = true;
            });
    }
}