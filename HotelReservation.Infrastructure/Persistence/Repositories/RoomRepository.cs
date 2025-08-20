using HotelReservation.Application.IRepository;
using HotelReservation.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HotelReservation.Infrastructure.Persistence.Repositories
{
    public class RoomRepository : GenericRepository<Room>, IRoomRepository
    {
        public RoomRepository(AppDbContext dbcontext) : base(dbcontext)
        {
        }

        public async Task<Room?> GetRoomDetailsAsync(Guid id)
        {
            var room = await
            _dbSet
            .Where(r => r.Id == id)
            .Include(r => r.RoomAmenities)
            .ThenInclude(ra => ra.Amenity)
            .FirstOrDefaultAsync();
            return room;

        }

        public async Task<List<Room>> GetAllRoomDetailsAsync()
        {
            var rooms = await
            _dbSet
            .Include(r => r.RoomAmenities)
            .ThenInclude(ra => ra.Amenity)
            .ToListAsync();
            return rooms;

        }
        
        public async Task<Room?> GetRoomWithBookingsAsync(Guid id)
        {
            var room = await
            _dbSet
            .Where(r => r.Id == id)
            .Include(r => r.Bookings)
            .FirstOrDefaultAsync();
            return room;
                        
        }


    }
}