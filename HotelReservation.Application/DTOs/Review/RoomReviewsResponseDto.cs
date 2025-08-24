namespace HotelReservation.Application.DTOs.Review
{
    public record class RoomReviewsResponseDto
    {
        public int ReviewsCount { get; init; }
        public double AverageRating{ get; init; }
        public IReadOnlyList<ReviewDetailDto> Reviews { get; init; } = null!;

    }
}