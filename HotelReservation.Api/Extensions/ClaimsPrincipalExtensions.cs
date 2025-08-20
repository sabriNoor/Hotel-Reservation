using System.Security.Claims;
using UnauthorizedAccessException = HotelReservation.Application.Exceptions.UnauthorizedAccessException;

namespace HotelReservation.Api.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static Guid GetUserId(this ClaimsPrincipal user)
        {
            var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userIdClaim == null || !Guid.TryParse(userIdClaim, out var userId))
                throw new UnauthorizedAccessException();
            return userId;
        }
    }
}