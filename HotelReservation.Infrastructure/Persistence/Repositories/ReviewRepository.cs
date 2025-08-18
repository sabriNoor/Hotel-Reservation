using HotelReservation.Application.IRepository;
using HotelReservation.Domain.Entities;

namespace HotelReservation.Infrastructure.Persistence.Repositories
{
    public class ReviewRepository : GenericRepository<Review>, IReviewRepository
    {
        public ReviewRepository(AppDbContext dbcontext) : base(dbcontext)
        {
        }
    }
}