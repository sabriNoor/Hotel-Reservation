using HotelReservation.Application.IRepository;
using HotelReservation.Domain.Entities;
using HotelReservation.Infrastructure.Persistence;

namespace HotelReservation.Infrastructure.Repositories
{
    public class AmenityRepository : GenericRepository<Amenity>, IAmenityRepository
    {
        public AmenityRepository(AppDbContext dbcontext) : base(dbcontext)
        {
        }
    }
}