
namespace HotelReservation.Application.DTOs.Review
{
    public record class ReviewDetailDto
    (
        Guid Id,
        int Rating,
        string? Comment,
        string UserFullName,
        string Username,
        string UserEmail
    );
}