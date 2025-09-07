using AutoMapper;
using HotelReservation.Application.DTOs.Amenity;
using HotelReservation.Application.Exceptions;
using HotelReservation.Application.IRepository;
using HotelReservation.Application.IServices;
using HotelReservation.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace HotelReservation.Application.Services
{
    public class AmenityService : IAmenityService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAmenityRepository _amenityRepository;
        private readonly ILogger<AmenityService> _logger;
        private readonly IMapper _mapper;

        public AmenityService(
            IUnitOfWork unitOfWork,
            ILogger<AmenityService> logger,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _amenityRepository = unitOfWork.AmenityRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<AmenityResponseDto> AddAmenityAsync(AmenityCURequestDto dto)
        {
            try
            {
                var amenity = _mapper.Map<Amenity>(dto);
                await _amenityRepository.AddAsync(amenity);
                await _unitOfWork.CompleteAsync();
                _logger.LogInformation("Amenity with ID {ID} added successfully", amenity.Id);
                return _mapper.Map<AmenityResponseDto>(amenity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error during adding amenity");
                throw;
            }
        }

        public async Task DeleteAmenityAsync(Guid id)
        {
            try
            {
                var amenity = await _amenityRepository.GetByIdAsync(id);
                if (amenity is null)
                {
                    throw new NotFoundException("Amenity", id);
                }

                await _amenityRepository.DeleteAsync(id);
                await _unitOfWork.CompleteAsync();
                _logger.LogInformation("Amenity with ID {ID} deleted successfully", id);
            }
            catch (NotFoundException ex)
            {
                _logger.LogWarning(ex, "Amenity with ID {ID} not found for deletion", id);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error during deleting amenity with ID {ID}", id);
                throw;
            }
        }

        public async Task<IReadOnlyList<AmenityResponseDto>> GetAllAmenityAsync()
        {
            try
            {
                var amenities = await _amenityRepository.GetAllAsync();
                _logger.LogInformation("Successfully retrieved {AmenityCount} amenities from the database.", amenities.Count());
                
                return _mapper.Map<IReadOnlyList<AmenityResponseDto>>(amenities);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error during getting all amenities");
                throw;
            }
        }

        public async Task<AmenityResponseDto> GetAmenityByIdAsync(Guid id)
        {
            try
            {
                var amenity = await _amenityRepository.GetByIdAsync(id);
                if (amenity is null)
                {
                    throw new NotFoundException("Amenity", id);
                }

                _logger.LogInformation("Amenity with ID {AmenityId} retrieved successfully.", id);

                return _mapper.Map<AmenityResponseDto>(amenity);
            }
            catch (NotFoundException ex)
            {
                _logger.LogWarning(ex, "Amenity with ID {AmenityId} was not found.", id);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error occurred while retrieving amenity with ID {AmenityId}.", id);
                throw;
            }
        }


        public async Task<AmenityResponseDto> UpdateAmenityAsync(Guid id, AmenityCURequestDto dto)
        {
            try
            {
                var amenity = await _amenityRepository.GetByIdAsync(id);
                if (amenity is null)
                {
                    throw new NotFoundException("Amenity", id);
                }

                _mapper.Map(dto, amenity);

                _amenityRepository.Update(amenity); 
                await _unitOfWork.CompleteAsync();

                _logger.LogInformation("Amenity with ID {ID} updated successfully", id);

                return _mapper.Map<AmenityResponseDto>(amenity);
            }
            catch (NotFoundException ex)
            {
                _logger.LogWarning(ex, "Amenity with ID {ID} not found during update", id);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error while updating Amenity with ID {ID}", id);
                throw;
            }
        }

    }
}