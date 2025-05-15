namespace ApiServices.Services;

using Refit;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.DependencyInjection;

public static class ApiService
{
    public static IServiceCollection ConfigureServices(IServiceCollection services, string baseApiUrl)
    {
        var refitSettings = new RefitSettings
        {
            ContentSerializer = new SystemTextJsonContentSerializer(
                new JsonSerializerOptions
                {
                    Converters = { new JsonStringEnumConverter() }
                })
        };

        typeof(ApiService).Assembly.GetTypes()
            .Where(t => t.IsInterface && t.Name.EndsWith("Api"))
            .ToList()
            .ForEach(apiType => services.AddSingleton(apiType, RestService.For(apiType, baseApiUrl, refitSettings)));

        return services;
    }
}