using HotelReservation.Application.IRepository;
using HotelReservation.Domain.Entities;
using HotelReservation.Infrastructure.Persistence;

namespace HotelReservation.Infrastructure.Repositories
{
    public class BookingRepository : GenericRepository<Booking>, IBookingRepository
    {
        public BookingRepository(AppDbContext dbcontext) : base(dbcontext)
        {
        }
    }
}