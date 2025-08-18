using HotelReservation.Application.IRepository;
using HotelReservation.Domain.Entities;

namespace HotelReservation.Infrastructure.Persistence.Repositories
{
    public class RoomRepository : GenericRepository<Room>, IRoomRepository
    {
        public RoomRepository(AppDbContext dbcontext) : base(dbcontext)
        {
        }
    }
}