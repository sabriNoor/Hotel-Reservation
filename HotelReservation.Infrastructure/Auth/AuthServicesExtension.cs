using HotelReservation.Application.IAuth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json;

namespace HotelReservation.Infrastructure.Auth
{
    public static class AuthServicesExtension
    {
        public static IServiceCollection AddAuthServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Configure JWT settings
            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

            // Add application services
            services.AddScoped<IJwtGenerator, JwtGenerator>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();

            // Add JWT authentication
            services.AddJwtAuthentication(configuration);

            return services;
        }

        private static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection("JwtSettings").Get<JwtSettings>()
                ?? throw new InvalidOperationException("JWT settings are not configured in appsettings.json.");


            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key))
                };
                options.Events = new JwtBearerEvents
                {
                    OnChallenge = context =>
                    {
                        // Customize response for missing/invalid token
                        context.HandleResponse();
                        context.Response.StatusCode = 401;
                        context.Response.ContentType = "application/json";
                        var result = JsonSerializer.Serialize(new
                        {
                            error = "Unauthorized",
                            details = context.ErrorDescription
                        });
                        return context.Response.WriteAsync(result);
                    }
                };
            });

            return services;
        }
    }
}
