using CloudFileSystem.Swagger.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Hosting;

namespace CloudFileSystem.Application.V1.Extensions;

/// <summary>
/// Extensions of <see cref="IApplicationBuilder" />
/// </summary>
public static class ApplicationBuilderExtensions
{
    /// <summary>
    /// Uses the application layer.
    /// </summary>
    /// <param name="app">The application.</param>
    /// <param name="env">The env.</param>
    /// <param name="provider">The provider.</param>
    /// <param name="apiName">Name of the API.</param>
    /// <returns></returns>
    public static IApplicationBuilder UseApplicationLayer(this IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider, string apiName)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app
            .UseCors(builder =>
            {
                builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .WithExposedHeaders("Cache-Control", "Content-Language", "Content-Length", "Content-Type", "Expires", "Last-Modified", "Pragma", "Content-Disposition");
            })
            .UseHttpsRedirection()
            .UseRouting()
            .UseAuthorization()
            .UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            })
            .UseSwaggerDocumentation(provider, apiName);

        return app;
    }
}