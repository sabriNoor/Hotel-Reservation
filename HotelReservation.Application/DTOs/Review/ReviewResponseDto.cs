namespace HotelReservation.Application.DTOs.Review
{
    public record class ReviewResponseDto
    (
        Guid Id,
        int Rating,
        string? Comment
    );
}