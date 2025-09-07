using FluentValidation;
using HotelReservation.Application.IServices;
using HotelReservation.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace HotelReservation.Application
{
  
    public static class DependencyInjection
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            // Register services
            services.AddScoped<IAmenityService, AmenityService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IBookingService, BookingService>();
            services.AddScoped<IReviewService, ReviewService>();
            services.AddScoped<IRoomService, RoomService>();
            services.AddScoped<IUserService, UserService>();

            // Register FluentValidation validators from this assembly
            services.AddValidatorsFromAssembly(AssemblyReference.Assembly);
            // Register AutoMapper profiles from this assembly
            services.AddAutoMapper(cfg =>{}, AssemblyReference.Assembly);

        }
    }
}
