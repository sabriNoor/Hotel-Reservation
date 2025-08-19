using AutoMapper;
using HotelReservation.Application.DTOs.User;
using HotelReservation.Application.Exceptions;
using HotelReservation.Application.IRepository;
using HotelReservation.Application.IServices;
using HotelReservation.Domain.ValueObjects;
using Microsoft.Extensions.Logging;

namespace HotelReservation.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UserService> _logger;
        private readonly IMapper _mapper;

        public UserService(
            IUnitOfWork unitOfWork,
            ILogger<UserService> logger,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _userRepository = unitOfWork.UserRepository;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<IReadOnlyList<UserProfileDto>> GetAllUserAsync()
        {
            try
            {
                var users = await _userRepository.GetAllAsync();
                _logger.LogInformation("Users retrived successfully with count {UserCounts}", users.Count());
                return (IReadOnlyList<UserProfileDto>)users.Select(u => _mapper.Map<UserProfileDto>(u));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error during getting all users");
                throw;
            }
        }

        public async Task<UserProfileDto> GetUserProfileByIdAsync(Guid id)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(id);
                if (user is null)
                {
                    throw new NotFoundException("User", id);
                }

                return _mapper.Map<UserProfileDto>(user);
            }
            catch (NotFoundException ex)
            {
                _logger.LogError(ex, "User with ID {ID} not found during getting profile", id);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error during getting profile for user with ID {ID}", id);
                throw;
            }
        }

        public async Task<UserProfileDto> UpdateUserProfileAsync(Guid id, UpdateUserDto dto)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(id);
                if (user is null)
                {
                    throw new NotFoundException("User", id);
                }
                var newUserPersonalInformation = _mapper.Map<PersonalInformation>(dto);
                user.PersonalInformation = newUserPersonalInformation;
                _userRepository.Update(user);
                await _unitOfWork.CompleteAsync();
                _logger.LogInformation("User information with username {Username} updated successfully", user.Username);
                return _mapper.Map<UserProfileDto>(user);

            }
            catch (NotFoundException ex)
            {
                _logger.LogError(ex, "User with ID {ID} not found during updating profile", id);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error during udpating user with ID {ID}", id);
                throw;
            }
        }

        public async Task DeleteUserAsync(Guid id)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(id);
                if (user is null)
                {
                    throw new NotFoundException("User", id);
                }

                await _userRepository.DeleteAsync(id);
                await _unitOfWork.CompleteAsync();
                _logger.LogInformation("User with ID {ID} deleted successfully", id);
            }
            catch (NotFoundException ex)
            {
                _logger.LogWarning(ex,"User with ID {ID} not found for deletion", id);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error during deleting user with ID {ID}", id);
                throw;
            }
        }

    }
}