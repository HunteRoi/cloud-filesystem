using CloudFileSystem.Application.Behaviours;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace CloudFileSystem.Application;

public static class ServiceCollectionExtensions
{
    public static void AddApplication(this IServiceCollection services)
    {
        var currentAssembly = typeof(ServiceCollectionExtensions).Assembly;
        services.AddAutoMapper(currentAssembly);
        services.AddMediatR(currentAssembly);
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        services.AddFluentValidation();
    }
}