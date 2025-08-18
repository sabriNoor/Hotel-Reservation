using HotelReservation.Application.IRepository;
using HotelReservation.Domain.Entities;
using HotelReservation.Infrastructure.Persistence;

namespace HotelReservation.Infrastructure.Repositories
{
    public class RoomRepository : GenericRepository<Room>, IRoomRepository
    {
        public RoomRepository(AppDbContext dbcontext) : base(dbcontext)
        {
        }
    }
}