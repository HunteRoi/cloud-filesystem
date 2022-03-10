using Microsoft.AspNetCore.Mvc;

namespace CloudFileSystem.Application.V1.Configuration;

/// <summary>
/// Configuration class of the services
/// </summary>
public class ServicesConfiguration
{
    /// <summary>
    /// Gets or sets the API version.
    /// </summary>
    /// <value>The API version.</value>
    public ApiVersion ApiVersion { get; set; }

    /// <summary>
    /// Gets or sets the type of the mapping profiles assembly.
    /// </summary>
    /// <value>The type of the mapping profiles assembly.</value>
    public Type MappingProfilesAssemblyType { get; set; }

    /// <summary>
    /// Gets or sets the type of the startup assembly.
    /// </summary>
    /// <value>The type of the startup assembly.</value>
    public Type StartupAssemblyType { get; set; }
}