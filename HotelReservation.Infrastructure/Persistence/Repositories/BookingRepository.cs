using HotelReservation.Application.IRepository;
using HotelReservation.Domain.Entities;
using HotelReservation.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace HotelReservation.Infrastructure.Persistence.Repositories
{
    public class BookingRepository : GenericRepository<Booking>, IBookingRepository
    {
        public BookingRepository(AppDbContext dbcontext) : base(dbcontext)
        {
        }

        public async Task<Booking?> GetBookingDetailAsync(Guid id)
        {
            var booking = await
            _dbSet
            .Where(b => b.Id == id)
            .Include(b => b.Room)
            .Include(b => b.User)
            .FirstOrDefaultAsync();
            return booking;
        }

        public IQueryable<Booking> GetBookingsWithDetails(
            DateTime? startDate = null,
            DateTime? endDate = null,
            BookingStatus? status = null)
        {
            var query = _dbSet.Include(b => b.Room)
                              .Include(b => b.User)
                              .AsQueryable();

            if (startDate.HasValue)
                query = query.Where(b => b.Stay.CheckIn >= startDate.Value);

            if (endDate.HasValue)
                query = query.Where(b => b.Stay.CheckOut <= endDate.Value);

            if (status.HasValue)
                query = query.Where(b => b.Status == status.Value);

            return query;
        }


    }
}