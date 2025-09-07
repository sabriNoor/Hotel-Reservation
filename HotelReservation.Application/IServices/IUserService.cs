using System.Linq.Expressions;
using HotelReservation.Application.DTOs.User;
using HotelReservation.Domain.Entities;

namespace HotelReservation.Application.IServices
{
    public interface IUserService
    {
        Task<UserProfileDto> GetUserProfileByIdAsync(Guid id);
        Task<UserProfileDto> UpdateUserProfileAsync(Guid id, UpdateUserDto dto);
        Task<IReadOnlyList<UserProfileDto>> GetAllUserAsync();
        Task DeleteUserAsync(Guid id);
        
    }
}