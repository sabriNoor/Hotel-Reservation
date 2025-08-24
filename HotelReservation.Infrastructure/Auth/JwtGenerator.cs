using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using HotelReservation.Application.IAuth;
using HotelReservation.Domain.Entities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace HotelReservation.Infrastructure.Auth
{
    public class JwtGenerator : IJwtGenerator
    {
        private readonly JwtSettings _jwtSettings;
        private readonly ILogger<JwtGenerator> _logger;
        public JwtGenerator(
            IOptions<JwtSettings> options,
            ILogger<JwtGenerator> logger
        )
        {
            _jwtSettings = options.Value;
            _logger = logger;
        }

        public string GenerateJwtToken(User user)
        {
            try
            {
                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role.ToString()),
                    new Claim(ClaimTypes.Email,user.Email),
                    new Claim(ClaimTypes.Name,user.Username)
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                        issuer: _jwtSettings.Issuer,
                        audience: _jwtSettings.Audience,
                        claims: claims,
                        expires: DateTime.Now.AddMinutes(_jwtSettings.ExpiresInMinutes),
                        signingCredentials: creds
                    );

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to generate JWT token for user with username {Username} and email {Email}", user.Username,user.Email);
                throw;
            }
        }
    }
}