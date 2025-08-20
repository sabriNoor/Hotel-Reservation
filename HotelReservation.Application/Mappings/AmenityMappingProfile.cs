using AutoMapper;
using HotelReservation.Application.DTOs.Amenity;
using HotelReservation.Domain.Entities;

namespace HotelReservation.Application.Mappings
{
    public class AmenityMappingProfile : Profile
    {
        public AmenityMappingProfile()
        {

            CreateMap<Amenity, AmenityResponseDto>();
             
            CreateMap<AmenityCURequestDto,Amenity >();
        
        }
    }
}