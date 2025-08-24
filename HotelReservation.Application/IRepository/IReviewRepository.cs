using HotelReservation.Domain.Entities;

namespace HotelReservation.Application.IRepository
{
    public interface IReviewRepository : IGenericRepository<Review>
    {
        Task<Review?> GetReviewDetailAsync(Guid id);
        Task<List<Review>> GetRoomReviewsAsync(Guid roomId);
    }
    
}