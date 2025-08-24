using HotelReservation.Application.DTOs.Review;

namespace HotelReservation.Application.IServices
{
    public interface IReviewService
    {
        Task<ReviewResponseDto> CreateReviewAsync(Guid userId, Guid bookingId, ReviewCURequestDto dto);
        Task<ReviewResponseDto> UpdateReviewAsync(Guid userId, Guid reviewId, ReviewCURequestDto dto);
        Task<RoomReviewsResponseDto> GetAllReviewsAsync(Guid roomId);
        Task DeleteReviewAsync(Guid userId, Guid reviewId);
    }
}
