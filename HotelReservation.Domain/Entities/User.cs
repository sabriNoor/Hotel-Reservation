using System.ComponentModel.DataAnnotations;
using HotelReservation.Domain.Enums;
using HotelReservation.Domain.ValueObjects;

namespace HotelReservation.Domain.Entities
{
    public class User : BaseEntity
    {
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Username is required.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 50 characters.")]
        public string Username { get; set; } = null!;

        [Required(ErrorMessage = "Password is required.")]
        public string PasswordHash { get; set; } = null!;

        public UserRole Role { get; set; } = UserRole.User;

        [Required]
        public PersonalInformation PersonalInformation { get; set; } = new PersonalInformation();

        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
