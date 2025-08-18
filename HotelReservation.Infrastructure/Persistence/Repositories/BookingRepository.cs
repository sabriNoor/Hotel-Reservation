using HotelReservation.Application.IRepository;
using HotelReservation.Domain.Entities;

namespace HotelReservation.Infrastructure.Persistence.Repositories
{
    public class BookingRepository : GenericRepository<Booking>, IBookingRepository
    {
        public BookingRepository(AppDbContext dbcontext) : base(dbcontext)
        {
        }
    }
}