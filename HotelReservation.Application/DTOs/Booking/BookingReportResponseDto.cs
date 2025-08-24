

using HotelReservation.Domain.Enums;

namespace HotelReservation.Application.DTOs.Booking
{
    public class BookingReportResponseDto
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int TotalBookings { get; init; }
        public decimal TotalRevenue { get; init; }
        public Dictionary<string, int>? BookingsByStatus { get; init; }
        
        
    }
}