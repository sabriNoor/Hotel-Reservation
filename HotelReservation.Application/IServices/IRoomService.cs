using HotelReservation.Application.DTOs.Room;

namespace HotelReservation.Application.IServices
{
    public interface IRoomService
    {
        Task<RoomResponseDto> UpdateRoomAsync(Guid id, RoomCURequestDto dto);
        Task<RoomResponseDto> AddRoomAsync(RoomCURequestDto dto);
        Task DeleteRoomAsync(Guid id);
        Task<RoomResponseDto> GetRoomByIdAsync(Guid id);
        Task<IReadOnlyList<RoomResponseDto>> GetAllRoomAsync();
    }
}