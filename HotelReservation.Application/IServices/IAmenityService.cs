using HotelReservation.Application.DTOs.Amenity;

namespace HotelReservation.Application.IServices
{
    public interface IAmenityService
    {
        Task<AmenityResponseDto> UpdateAmenityAsync(Guid id, AmenityCURequestDto dto);
        Task<AmenityResponseDto> AddAmenityAsync(AmenityCURequestDto dto);
        Task DeleteAmenityAsync(Guid id);
        Task<AmenityResponseDto> GetAmenityByIdAsync(Guid id);
        Task<IReadOnlyList<AmenityResponseDto>> GetAllAmenityAsync();

    }
}