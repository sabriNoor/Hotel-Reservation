using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelReservation.Application.DTOs.Booking
{
    public record class BookingReportRequestDto
    {
        public DateTime StartDate { get; init; }
        public DateTime EndDate{ get; init; }
        
    }
}