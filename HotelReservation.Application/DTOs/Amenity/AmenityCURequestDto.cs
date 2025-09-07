namespace HotelReservation.Application.DTOs.Amenity
{
    public record class AmenityCURequestDto
    {
        public string Name { get; init; } =string.Empty;
        public string? Description { get; init; }
    }
}