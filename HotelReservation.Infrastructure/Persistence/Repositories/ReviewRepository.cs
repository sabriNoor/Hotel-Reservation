using HotelReservation.Application.IRepository;
using HotelReservation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;

namespace HotelReservation.Infrastructure.Persistence.Repositories
{
    public class ReviewRepository : GenericRepository<Review>, IReviewRepository
    {
        public ReviewRepository(AppDbContext dbcontext,IDistributedCache cache) : base(dbcontext,cache)
        {
        }

        public async Task<Review?> GetReviewDetailAsync(Guid id)
        {
            return await
            _dbSet
            .Where(r=>r.Id==id)
            .Include(r => r.Booking)
            .ThenInclude(b => b.User)
            .FirstOrDefaultAsync();

        }

        public async Task<List<Review>> GetRoomReviewsAsync(Guid roomId)
        {
            return await
            _dbSet
            .Include(r => r.Booking)
            .ThenInclude(b => b.User)
            .ThenInclude(u => u.PersonalInformation)
            .Where(r=>r.Booking.RoomId==roomId)
            .ToListAsync();
            

        }
    }
}