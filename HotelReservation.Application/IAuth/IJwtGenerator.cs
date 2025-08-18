using HotelReservation.Domain.Entities;

namespace HotelReservation.Application.IAuth
{
    public interface IJwtGenerator
    {
        string GenerateJwtToken(User user);
    }
}