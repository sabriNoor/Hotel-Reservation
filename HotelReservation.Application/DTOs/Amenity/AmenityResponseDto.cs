namespace HotelReservation.Application.DTOs.Amenity
{
    public record class AmenityResponseDto
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public string? Description { get; init; }
    }
}