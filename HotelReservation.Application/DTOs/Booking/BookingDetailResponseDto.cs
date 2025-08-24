using HotelReservation.Application.DTOs.Common;
using HotelReservation.Domain.Enums;

namespace HotelReservation.Application.DTOs.Booking
{
    public class BookingDetailResponseDto
    {
        public Guid BookingId { get; init; }
        public int RoomNumber { get; init; }
        public int NumberOfGuests { get; init; }
        public string? Notes { get; init; }
        public DateRangeDto Stay { get; init; } = null!;
        public BookingStatus Status { get; init; }
        public MoneyDto TotalPrice { get; init; } = null!;
        public string FullName { get; init; } = string.Empty;
        public string Email { get; init; } = string.Empty;
        public string MobileNumber { get; init; } = string.Empty;
        public DateTime CreatedAt { get; init; }
    }
}