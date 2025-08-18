namespace HotelReservation.Application.DTOs.Auth
{
    public record class RegisterRequestDto
    {

        public string Email { get; init; } = string.Empty;
        public string Username { get; init; } = string.Empty;
        public string Password { get; init; } = string.Empty;
        public string ConfirmPassword { get; init; } = string.Empty;
        public string FirstName { get; init; } = string.Empty;
        public string LastName { get; init; } = string.Empty;
        public string MobileNumber { get; init; } = string.Empty;
        
    }
}