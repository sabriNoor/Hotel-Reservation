using HotelReservation.Domain.Enums;

namespace HotelReservation.Application.DTOs.Booking
{
    public record class BookingStatusUpdateRequestDto
    {
        public BookingStatus Status { get; init; }
    }
}