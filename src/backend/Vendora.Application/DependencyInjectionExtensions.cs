namespace Vendora.Application;

using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var executionAssembly = Assembly.GetExecutingAssembly();

        services.AddMediatR(config
            => config.RegisterServicesFromAssembly(executionAssembly));

        services.AddAutoMapper(executionAssembly);

        return services;
    }
}