using CorpResource.APIClient.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CorpResource.APIClient.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCorpResourceServices(this IServiceCollection services, IConfiguration configuration)
    {
        var apiUrl = configuration["Api:BaseUrl"] ?? 
            throw new InvalidOperationException("API URL not configured.");

        if (!apiUrl.EndsWith('/')) apiUrl += "/";

        services.AddHttpClient("CorpResourceApi", client =>
        {
            client.BaseAddress = new Uri(apiUrl, UriKind.Absolute);
        })
        .ConfigurePrimaryHttpMessageHandler(() =>
        {
            return new HttpClientHandler
            {
                UseDefaultCredentials = true
            };
        });

        services.AddScoped<IUsersApiService, UsersApiService>();
        services.AddScoped<IDepartmentsApiService, DepartmentsApiService>();

        return services;
    }
}
