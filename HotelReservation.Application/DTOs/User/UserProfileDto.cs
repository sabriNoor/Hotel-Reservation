namespace HotelReservation.Application.DTOs.User
{
    public class UserProfileDto
    {
        public Guid UserId { get; init; }       
        public string Username { get; init; } = string.Empty;  
        public string Email { get; init; }= string.Empty;  
        public string FullName { get; init; }= string.Empty; 
        public string MobileNumber { get; init; }= string.Empty; 
        public DateTime CreatedAt { get; init; }
        
    }
}