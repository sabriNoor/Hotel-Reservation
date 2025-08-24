namespace HotelReservation.Application.DTOs.Auth
{
    public record class LoginResponseDto
    {
        public string Token { get; init; } = string.Empty;
        public string Name { get; init; } = string.Empty;
    }
}