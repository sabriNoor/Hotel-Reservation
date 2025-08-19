using HotelReservation.Application.DTOs.Common;
using HotelReservation.Domain.Enums;

namespace HotelReservation.Application.DTOs.Room
{
    public record class RoomCURequestDto
    {
        public int Number { get; init; }

        public RoomType Type { get; init; }

        public string Description { get; init; } = string.Empty;

        public MoneyDto PricePerNight { get; init; } = null!;
        public int Capacity { get; init; }

        public bool IsAvailable { get; init; } = true;

        public List<Guid> AmenityIDs { get; init; } = new List<Guid>();
    }
}