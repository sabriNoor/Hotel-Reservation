namespace HotelReservation.Application.DTOs.Review
{
    public record class ReviewCURequestDto
    {
        public int Rating { get; set; }
        
        public string? Comment { get; set; }
    }
}