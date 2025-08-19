using AutoMapper;
using HotelReservation.Application.DTOs.Common;
using HotelReservation.Application.DTOs.Room;
using HotelReservation.Domain.Entities;
using HotelReservation.Domain.ValueObjects;

namespace HotelReservation.Application.Mappings
{
    public class RoomMappingProfile : Profile
    {
        public RoomMappingProfile()
        {
            CreateMap<MoneyDto, Money>();

            CreateMap<RoomCURequestDto, Room>()
                .ForMember(dst => dst.PricePerNight,
                    opt => opt.MapFrom(src => src.PricePerNight)
                );

            CreateMap<Room, RoomResponseDto>()
               .ForMember(dst => dst.PricePerNight,
                   opt => opt.MapFrom(src => src.PricePerNight))
                .ForMember(dst=>dst.Amenities,
                    opt=>opt.MapFrom(src => src.RoomAmenities.Select(ra => ra.Amenity)))
              ; 
        }
        
    }
}