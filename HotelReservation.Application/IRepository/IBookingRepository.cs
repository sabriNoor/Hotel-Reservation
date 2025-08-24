using HotelReservation.Domain.Entities;
using HotelReservation.Domain.Enums;

namespace HotelReservation.Application.IRepository
{
    public interface IBookingRepository : IGenericRepository<Booking>
    {
        Task<Booking?> GetBookingDetailAsync(Guid id);
        public IQueryable<Booking> GetBookingsWithDetails(
           DateTime? startDate = null,
           DateTime? endDate = null,
           BookingStatus? status = null);
    }
}