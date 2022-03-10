using CloudFileSystem.Application.V1.Configuration;
using CloudFileSystem.Application.V1.Extensions;
using CloudFileSystem.Application.V1.Mappings;
using CloudFileSystem.Infrastructure.V1.Extensions;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var servicesConfig = new ServicesConfiguration
{
    ApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0),
    MappingProfilesAssemblyType = typeof(MappingProfile),
    StartupAssemblyType = typeof(Program)
};

builder.Services
    .AddInfrastructureLayer()
    .AddApplicationLayer(servicesConfig);

var app = builder.Build();
IWebHostEnvironment webhostEnvironment = app.Environment;
var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

app.UseApplicationLayer(webhostEnvironment, apiVersionDescriptionProvider, "Cloud FileSystem");

app.Run();