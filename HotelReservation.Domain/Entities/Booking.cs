using System.ComponentModel.DataAnnotations;
using HotelReservation.Domain.Enums;
using HotelReservation.Domain.ValueObjects;

namespace HotelReservation.Domain.Entities
{
    public class Booking : BaseEntity
    {
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;
        public Guid RoomId { get; set; }
        public Room Room { get; set; } = null!;

        [Required(ErrorMessage = "Date of stay is required.")]
        public DateRange Stay { get; set; } = new DateRange();

        [Range(1, int.MaxValue, ErrorMessage = "Number of guests must be at least 1.")]
        public int NumberOfGuests { get; set; }

        [Required]
        public Money TotalPrice { get; set; } = null!;

        public string? Notes { get; set; }

        public BookingStatus Status { get; set; } = BookingStatus.Pending;

        public Guid? ReviewId { get; set; }
        public Review? Review { get; set; }
        
        public void CalculateTotalPrice()
        {
            TotalPrice = new Money()
            {
                Amount = Room.PricePerNight.Amount * Stay.GetNights(),
                Currency = Room.PricePerNight.Currency
            };
        }


        
    }

   
}
