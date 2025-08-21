using AutoMapper;
using HotelReservation.Application.DTOs.Room;
using HotelReservation.Application.Exceptions;
using HotelReservation.Application.IRepository;
using HotelReservation.Application.IServices;
using HotelReservation.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace HotelReservation.Application.Services
{
    public class RoomService : IRoomService
    {
        private readonly ILogger<RoomService> _logger;
        private readonly IMapper _mapper;
        private readonly IRoomRepository _roomRepository;
        private readonly IUnitOfWork _unitOfWork;
        public RoomService(
            ILogger<RoomService> logger,
            IMapper mapper,
            IUnitOfWork unitOfWork
        )
        {
            _logger = logger;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _roomRepository = _unitOfWork.RoomRepository;

        }
        public async Task<RoomResponseDto> AddRoomAsync(RoomCURequestDto dto)
        {
            try
            {
                var roomAmenities = await GetRoomAmenitiesAsync(dto.AmenityIDs);
                var room = _mapper.Map<Room>(dto);
                var existRoom = await _roomRepository.ExistsAsync(r => r.Number == dto.Number);
                if (existRoom)
                    throw new BusinessException("Room already exist");
                room.RoomAmenities = roomAmenities;
                await _roomRepository.AddAsync(room);
                await _unitOfWork.CompleteAsync();
                _logger.LogInformation("Room with ID {ID} added successfully", room.Id);
                var newRoom = await _roomRepository.GetRoomDetailsAsync(room.Id)
                    ?? throw new Exception("Failed to load room after adding.");
                return _mapper.Map<RoomResponseDto>(newRoom);

            }
            catch (BusinessException ex)
            {
                _logger.LogError(ex, "Room with Number {RoomNumber} already exist",dto.Number);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error during adding room");
                throw;
            }
        }
        private async Task<List<RoomAmenity>> GetRoomAmenitiesAsync(List<Guid> amenityIds)
        {
            var amenities = await _unitOfWork.AmenityRepository
                .FindAsync(a => amenityIds.Contains(a.Id));

            var roomAmenities = amenities
                .Select(a => new RoomAmenity { AmenityId = a.Id })
                .ToList();

            var missingIds = amenityIds.Except(amenities.Select(a => a.Id));
            foreach (var id in missingIds)
            {
                _logger.LogWarning("Amenity with ID {ID} not found", id);
            }

            return roomAmenities;
        }


        public async Task DeleteRoomAsync(Guid id)
        {
            try
            {
                var room = await _roomRepository.GetByIdAsync(id);
                if (room is null)
                {
                    throw new NotFoundException("Room", id);
                }

                await _roomRepository.DeleteAsync(id);
                await _unitOfWork.CompleteAsync();
                _logger.LogInformation("Room with ID {ID} deleted successfully", id);
            }
            catch (NotFoundException ex)
            {
                _logger.LogWarning(ex, "Room with ID {ID} not found for deletion", id);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error during deleting room with ID {ID}", id);
                throw;
            }
        }

        public async Task<IReadOnlyList<RoomResponseDto>> GetAllRoomAsync()
        {
            try
            {
                var rooms = await _roomRepository.GetAllRoomDetailsAsync();
                _logger.LogInformation("Successfully retrieved {RoomCount} amenities from the database.", rooms.Count());


                return _mapper.Map<IReadOnlyList<RoomResponseDto>>(rooms);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error during getting all rooms");
                throw;
            }
        }

        public async Task<RoomResponseDto> GetRoomByIdAsync(Guid id)
        {
            try
            {
                var room = await _roomRepository.GetRoomDetailsAsync(id);
                if (room is null)
                {
                    throw new NotFoundException("Room", id);
                }

                _logger.LogInformation("Room with ID {RoomId} retrieved successfully.", id);

                return _mapper.Map<RoomResponseDto>(room);
            }
            catch (NotFoundException ex)
            {
                _logger.LogWarning(ex, "Room with ID {RoomId} was not found.", id);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error occurred while retrieving room with ID {RoomId}.", id);
                throw;
            }
        }

        public async Task<RoomResponseDto> UpdateRoomAsync(Guid id, RoomCURequestDto dto)
        {
            try
            {
                var room = await _roomRepository.GetByIdAsync(id);
                if (room is null)
                {
                    throw new NotFoundException("Room", id);
                }
                var roomAmenities = await GetRoomAmenitiesAsync(dto.AmenityIDs);
                _mapper.Map(dto, room);
                room.RoomAmenities = roomAmenities;
                _roomRepository.Update(room);
                await _unitOfWork.CompleteAsync();
                _logger.LogInformation("Room with ID {RoomId} added successfully", room.Id);
                var updatedRoom = await _roomRepository.GetRoomDetailsAsync(room.Id)
                    ?? throw new Exception("Failed to load room after adding.");
                return _mapper.Map<RoomResponseDto>(updatedRoom);

            }
            catch (NotFoundException ex)
            {
                _logger.LogError(ex, "Room with ID {RoomId} not found during update", id);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error during adding room");
                throw;
            }
        }

        public async Task<IReadOnlyList<RoomResponseDto>> GetAvailableRoomsAsync(RoomFilterDto filterDto)
        {
            try
            {
                var rooms = await _roomRepository.GetRoomAvailableAsync(filterDto.StartDate, filterDto.EndDate);

                _logger.LogInformation(
                    "Retrieved {Count} available rooms for dates: start={StartDate}, end={EndDate}",
                    rooms.Count, filterDto.StartDate, filterDto.EndDate
                );

                return _mapper.Map<IReadOnlyList<RoomResponseDto>>(rooms);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Error retrieving available rooms for dates: start={StartDate}, end={EndDate}",
                    filterDto.StartDate, filterDto.EndDate);
                throw; 
            }
        }

    }
}