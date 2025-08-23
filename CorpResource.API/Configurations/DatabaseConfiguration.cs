using CorpResource.Application.Contants;
using CorpResource.Infrastructure.Persistance.Context;
using Microsoft.EntityFrameworkCore;

namespace CorpResource.API.Configurations;

public static class DatabaseConfiguration
{
    public static void AddCustomDatabaseConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString(ApiConstants.DatabaseConnString);

        services.AddDbContext<CorpResourceDbContext>(options =>
        {
            options.UseSqlServer(connectionString, sqlServerOptions =>
            {
                var assembly = typeof(CorpResourceDbContext).Assembly;
                var assemblyName = assembly.GetName();

                sqlServerOptions.MigrationsAssembly(assemblyName.Name);
                sqlServerOptions.EnableRetryOnFailure(
                    maxRetryCount: 2,
                    maxRetryDelay: TimeSpan.FromSeconds(30),
                    errorNumbersToAdd: null);
            });
        });
    }
}
