namespace Vendora.Infrastructure;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Vendora.Application.Common;
using Vendora.Infrastructure.Persistence.EntityFramework;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<IAppDbContext, AppDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString(name: "DefaultConnection")));

        return services;
    }
}