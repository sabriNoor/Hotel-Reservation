using System.ComponentModel.DataAnnotations;
using HotelReservation.Domain.Enums;
using HotelReservation.Domain.ValueObjects;

namespace HotelReservation.Domain.Entities
{
    public class Room : BaseEntity
    {
        [Range(1, int.MaxValue, ErrorMessage = "Room number must be greater than 0.")]
        public int Number { get; set; }

        [Required(ErrorMessage = "Room type is required.")]
        public RoomType Type { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [MaxLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Price per night is required.")]
        public Money PricePerNight { get; set; } = null!;

        [Range(1, 100)]
        public int Capacity { get; set; }

        public bool IsAvailable { get; set; } = true;

        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
        public ICollection<RoomAmenity> RoomAmenities  { get; set; } = new List<RoomAmenity>();
    }
}
