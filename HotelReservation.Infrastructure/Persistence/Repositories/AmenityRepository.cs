using HotelReservation.Application.IRepository;
using HotelReservation.Domain.Entities;

namespace HotelReservation.Infrastructure.Persistence.Repositories
{
    public class AmenityRepository : GenericRepository<Amenity>, IAmenityRepository
    {
        public AmenityRepository(AppDbContext dbcontext) : base(dbcontext)
        {
        }
    }
}