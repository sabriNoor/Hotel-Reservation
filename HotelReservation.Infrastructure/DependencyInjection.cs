using HotelReservation.Infrastructure.Auth;
using HotelReservation.Infrastructure.Cache;
using HotelReservation.Infrastructure.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HotelReservation.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
             // Persistence (DbContext, Repositories)
            services.AddPersistenceServices(configuration);
            
            // Auth services
            services.AddAuthServices(configuration);

            // Cache services
            services.AddCacheService(configuration);
           

            return services;
        }
    }
}
