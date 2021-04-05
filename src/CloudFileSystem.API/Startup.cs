using CloudFileSystem.Application.V1.Extensions;
using CloudFileSystem.Application.V1.Mappings;
using CloudFileSystem.Infrastructure.V1.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CloudFileSystem.API
{
    /// <summary>
    /// Startup class
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <value>
        /// The configuration.
        /// </value>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Configures the services.
        /// </summary>
        /// <param name="services">The services.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            var servicesConfig = new Application.V1.Configuration.ServicesConfiguration
            {
                ApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0),
                MappingProfilesAssemblyType = typeof(MappingProfile),
                StartupAssemblyType = typeof(Startup)
            };

            services
                .AddInfrastructureLayer()
                .AddApplicationLayer(servicesConfig);
        }

        /// <summary>
        /// Configures the specified application.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="env">The env.</param>
        /// <param name="provider">The provider.</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            app.UseApplicationLayer(env, provider, "Cloud FileSystem");
        }
    }
}
