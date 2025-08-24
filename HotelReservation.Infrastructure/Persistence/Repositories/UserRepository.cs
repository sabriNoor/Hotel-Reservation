using HotelReservation.Application.IRepository;
using HotelReservation.Domain.Entities;
using Microsoft.Extensions.Caching.Distributed;

namespace HotelReservation.Infrastructure.Persistence.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(AppDbContext dbcontext,IDistributedCache cache) : base(dbcontext,cache)
        {
        }
    }
}