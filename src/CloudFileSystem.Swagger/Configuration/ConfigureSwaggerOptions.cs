using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace CloudFileSystem.Swagger.Configuration;

/// <summary>
/// Swagger configuration options
/// </summary>
/// <seealso cref="Microsoft.Extensions.Options.IConfigureOptions{Swashbuckle.AspNetCore.SwaggerGen.SwaggerGenOptions}" />
public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
{
    /// <summary>
    /// The provider
    /// </summary>
    private readonly IApiVersionDescriptionProvider _provider;

    /// <summary>
    /// Initializes a new instance of the <see cref="ConfigureSwaggerOptions" /> class.
    /// </summary>
    /// <param name="provider">
    /// The <see cref="IApiVersionDescriptionProvider">provider</see> used to generate Swagger documents.
    /// </param>
    public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider) => _provider = provider;

    /// <summary>
    /// Gets the XML comments file path.
    /// </summary>
    /// <value>The XML comments file path.</value>
    private static string XmlCommentsFilePath
    {
        get
        {
            var basePath = PlatformServices.Default.Application.ApplicationBasePath;
            var fileName = Assembly.GetEntryAssembly()?.GetName().Name + ".xml";
            return Path.Combine(basePath, fileName);
        }
    }

    /// <summary>
    /// Creates the information for API version.
    /// </summary>
    /// <param name="description">The description.</param>
    /// <returns></returns>
    protected static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
    {
        var attributes = Assembly.GetEntryAssembly()?.GetCustomAttributes(typeof(AssemblyProductAttribute), false);
        AssemblyProductAttribute attribute = null;
        if (attributes.Length > 0)
        {
            attribute = attributes[0] as AssemblyProductAttribute;
        }

        var info = new OpenApiInfo()
        {
            Title = $"{attribute?.Product} | API Documentation",
            Version = description.ApiVersion.ToString(),
            Description = $"The RESTful API web documentation for {attribute?.Product} v{description.ApiVersion:VV}",
            License = new OpenApiLicense()
            {
                Name = "MIT",
                Url = new Uri("https://github.com/hunteroi/cloud-filesystem/blob/master/LICENSE")
            }
        };

        if (description.IsDeprecated)
        {
            info.Description += " This API version has been deprecated.";
        }

        return info;
    }

    /// <inheritdoc />
    public void Configure(SwaggerGenOptions options)
    {
        foreach (var description in _provider.ApiVersionDescriptions)
        {
            options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
        }

        options.CustomSchemaIds(x => x.FullName);

        options.DescribeAllParametersInCamelCase();

        try
        {
            options.IncludeXmlComments(XmlCommentsFilePath);
        }
        catch
        {
            // ignored
        }

        options.EnableAnnotations();
    }
}