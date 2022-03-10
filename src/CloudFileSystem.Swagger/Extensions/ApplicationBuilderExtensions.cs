using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace CloudFileSystem.Swagger.Extensions;

/// <summary>
/// Swagger extensions for <see cref="IApplicationBuilder" />.
/// </summary>
public static class ApplicationBuilderExtensions
{
    /// <summary>
    /// Uses the swagger documentation.
    /// </summary>
    /// <param name="app">The application.</param>
    /// <param name="provider">The provider.</param>
    /// <param name="apiTitle">The API title.</param>
    /// <returns></returns>
    public static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app, IApiVersionDescriptionProvider provider, string apiTitle)
    {
        return app
            .UseSwagger()
            .UseSwaggerUI(options =>
            {
                options.RoutePrefix = string.Empty;
                options.DocumentTitle = apiTitle;
                options.DefaultModelExpandDepth(1);
                options.DefaultModelsExpandDepth(-1);
                options.DefaultModelRendering(Swashbuckle.AspNetCore.SwaggerUI.ModelRendering.Example);
                options.EnableDeepLinking();
                options.ShowExtensions();
                options.DisplayRequestDuration();
                options.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.List);

                foreach (var description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                }
            });
    }
}