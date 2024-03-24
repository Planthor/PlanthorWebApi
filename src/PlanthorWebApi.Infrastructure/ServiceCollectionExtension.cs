using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace PlanthorWebApi.Infrastructure;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddPlanthorDbContext(
        this IServiceCollection services,
        string connectionString)
    {
        services.AddDbContext<PlanthorDbContext>(options =>
        {
            options.UseMongoDB(connectionString, "planthor");
        });

        return services;
    }
}
