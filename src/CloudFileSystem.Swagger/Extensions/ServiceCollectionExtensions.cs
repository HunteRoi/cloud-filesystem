using CloudFileSystem.Swagger.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CloudFileSystem.Swagger.Extensions
{
    /// <summary>
    /// Swagger extensions for <see cref="IServiceCollection"/>
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the swagger documentation.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <returns></returns>
        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
        {
            return services
                .AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>()
                .AddSwaggerGen(options => 
                {
                    options.OperationFilter<SwaggerDefaultValues>();
                })
                .AddSwaggerGenNewtonsoftSupport();
        }
    }
}
