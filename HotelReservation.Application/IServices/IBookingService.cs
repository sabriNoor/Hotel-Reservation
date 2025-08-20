using HotelReservation.Application.DTOs.Booking;
using HotelReservation.Application.DTOs.Common;
using HotelReservation.Domain.Enums;

namespace HotelReservation.Application.IServices
{
    public interface IBookingService
    {
        Task<BookingResponseDto> CreateBookingAsync(Guid userId, BookingCreateRequestDto dto);
        Task<BookingResponseDto> UpdateBookingStatusAsync(Guid bookingId, BookingStatusUpdateRequestDto dto);
        Task<BookingDetailResponseDto> GetBookingByIdAsync(Guid bookingId);
        Task<IReadOnlyList<BookingResponseDto>> GetUserBookingsAsync(Guid userId);
        Task<IReadOnlyList<BookingDetailResponseDto>> GetBookingsAsync(BookingFilterDto filterDto);
        Task<BookingReportResponseDto> GetBookingReportAsync(BookingReportRequestDto dto);
        
    }
}