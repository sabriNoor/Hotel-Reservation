using System.ComponentModel.DataAnnotations;
using HotelReservation.Domain.Enums;
using HotelReservation.Domain.ValueObjects;

namespace HotelReservation.Domain.Entities
{
    public class Booking : BaseEntity
    {
        [Required]
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;

        [Required]
        public Guid RoomId { get; set; } 
        public Room Room { get; set; } = null!;

        [Required(ErrorMessage = "Date of stay is required.")]
        public DateRange Stay { get; set; } = new DateRange();

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Number of guests must be at least 1.")]
        public int NumberOfGuests { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Total price must be at least 0.")]
        public decimal TotalPrice { get; set; }

        public string? Notes { get; set; }

        public BookingStatus Status { get; set; } = BookingStatus.Pending;
    }

   
}
