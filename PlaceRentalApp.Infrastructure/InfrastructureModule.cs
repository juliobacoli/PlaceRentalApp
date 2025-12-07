using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PlaceRentalApp.Infrastructure.Persistence;

namespace PlaceRentalApp.Infrastructure;

public static class InfrastructureModule
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPersistence(configuration);

        return services;
    }

    private static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("PlaceRental");

        if (string.IsNullOrEmpty(connectionString))
            throw new InvalidOperationException("Connection string 'PlaceRental' not found.");

        services.AddDbContext<PlaceRentalDbContext>(o => o.UseSqlServer(connectionString));

        return services;
    }
}
