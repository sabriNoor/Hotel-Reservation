using HotelReservation.Application.IRepository;
using HotelReservation.Domain.Entities;

namespace HotelReservation.Infrastructure.Persistence.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(AppDbContext dbcontext) : base(dbcontext)
        {
        }
    }
}