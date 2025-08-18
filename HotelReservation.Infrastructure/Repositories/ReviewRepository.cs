using HotelReservation.Application.IRepository;
using HotelReservation.Domain.Entities;
using HotelReservation.Infrastructure.Persistence;

namespace HotelReservation.Infrastructure.Repositories
{
    public class ReviewRepository : GenericRepository<Review>, IReviewRepository
    {
        public ReviewRepository(AppDbContext dbcontext) : base(dbcontext)
        {
        }
    }
}