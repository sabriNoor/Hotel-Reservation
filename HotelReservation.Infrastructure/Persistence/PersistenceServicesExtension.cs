using System.Threading.Tasks;
using HotelReservation.Application.IRepository;
using HotelReservation.Infrastructure.Persistence.Data;
using HotelReservation.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HotelReservation.Infrastructure.Persistence
{
    public static class PersistenceServicesExtension
    {
        public static void AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("AppDbContextConnection") ??
                    throw new ArgumentException("AppDbContextConnection not found"));
            });

            
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAmenityRepository, AmenityRepository>();
            services.AddScoped<IBookingRepository, BookingRepository>();
            services.AddScoped<IReviewRepository, ReviewRepository>();
            services.AddScoped<IRoomRepository, RoomRepository>();
            services.AddScoped<IUserRepository, UserRepository>();



        }

        public static async Task MigrateAndSeedDatabase(this IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            db.Database.Migrate();

            await DataSeeder.SeedAsync(db);
        }
    }
}