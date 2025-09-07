using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HotelReservation.Infrastructure.Cache
{
    public static class CacheServiceExtension
    {
        public static void AddCacheService(this IServiceCollection services, IConfiguration configuration)
        {
            
            services.AddStackExchangeRedisCache(options=>
            {
                options.Configuration = configuration.GetConnectionString("Redis");
                options.InstanceName = "Redis.Core.WebApi_HotelReservation";
            });

        }
    }
}