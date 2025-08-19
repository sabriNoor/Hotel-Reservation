using HotelReservation.Application.DTOs.Amenity;
using HotelReservation.Application.DTOs.Common;
using HotelReservation.Domain.Enums;

namespace HotelReservation.Application.DTOs.Room
{
    public record class RoomResponseDto 
    {
        public int Number { get; init; }

        public RoomType Type { get; init; }

        public string Description { get; init; } = string.Empty;

        public MoneyDto PricePerNight { get; init; } = null!;
        public int Capacity { get; init; }

        public bool IsAvailable { get; init; } = true;
        public List<AmenityResponseDto> Amenities { get; init; } = new List<AmenityResponseDto>();
    }
}