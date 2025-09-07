
namespace HotelReservation.Application.DTOs.Review
{
    public record class ReviewDetailDto
    {
        public Guid Id { get; init; }
        public int Rating { get; init; }
        public string? Comment { get; init; }
        public string UserFullName { get; init; } = string.Empty;
        public string Username { get; init; } = string.Empty;
        public string UserEmail { get; init; } = string.Empty;
    }

}