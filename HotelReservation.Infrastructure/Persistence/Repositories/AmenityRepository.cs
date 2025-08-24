using HotelReservation.Application.IRepository;
using HotelReservation.Domain.Entities;
using Microsoft.Extensions.Caching.Distributed;

namespace HotelReservation.Infrastructure.Persistence.Repositories
{
    public class AmenityRepository : GenericRepository<Amenity>, IAmenityRepository
    {
        public AmenityRepository(AppDbContext dbcontext,IDistributedCache cache) : base(dbcontext,cache)
        {
        }
    }
}