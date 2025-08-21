namespace HotelReservation.Application.DTOs.Auth
{
    public record class LoginRequestDto
    {
        public string Username { get; init; } = string.Empty;
        public string Password { get; init; } = string.Empty; 
        
    }
}