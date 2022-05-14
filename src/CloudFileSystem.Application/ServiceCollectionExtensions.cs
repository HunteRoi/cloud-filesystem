using CloudFileSystem.Application.Behaviours;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace CloudFileSystem.Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var currentAssembly = typeof(ServiceCollectionExtensions).Assembly;

        services.AddAutoMapper(currentAssembly);
        services.AddMediatR(currentAssembly);
        services.AddFluentValidation(config => config.RegisterValidatorsFromAssembly(currentAssembly));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

        return services;
    }
}