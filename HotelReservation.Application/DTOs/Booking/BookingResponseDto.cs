using HotelReservation.Application.DTOs.Common;
using HotelReservation.Domain.Enums;

namespace HotelReservation.Application.DTOs.Booking
{
    public class BookingResponseDto
    {
        public Guid RoomId { get; init; }
        public int NumberOfGuests { get; init; }
        public string? Notes { get; init; }
        public DateRangeDto Stay { get; init; } = null!;
        public BookingStatus Status { get; init; }
        public MoneyDto TotalPrice { get; init; } = null!;
    }
}