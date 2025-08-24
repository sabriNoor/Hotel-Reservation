using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelReservation.Application.DTOs.Common;

namespace HotelReservation.Application.DTOs.Booking
{
    public record class BookingCreateRequestDto
    {
        public Guid RoomId { get; init; }
        public int NumberOfGuests { get; init; }
        public string? Notes { get; init; }
        public DateRangeDto Stay { get; init; } = null!;
    }
}