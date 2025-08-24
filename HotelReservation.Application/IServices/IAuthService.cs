using HotelReservation.Application.DTOs.Auth;

namespace HotelReservation.Application.IServices
{
    public interface IAuthService
    {
        Task<LoginResponseDto> LoginAsync(LoginRequestDto dto);
        Task<RegisterResponseDto> RegisterAsync(RegisterRequestDto dto);
    }
}