using AutoMapper;
using HotelReservation.Application.DTOs.User;
using HotelReservation.Domain.Entities;
using HotelReservation.Domain.ValueObjects;

namespace HotelReservation.Application.Mappings
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<User, UserProfileDto>()
             .ForMember(dest => dest.UserId,
                opt => opt.MapFrom(src => src.Id))
            .ForMember(dst => dst.FullName,
                opt => opt.MapFrom(src => $"{src.PersonalInformation.FirstName} {src.PersonalInformation.LastName}"))
            .ForMember(dst => dst.MobileNumber,
                opt => opt.MapFrom(src => src.PersonalInformation.MobileNumber));

            CreateMap<UpdateUserDto, PersonalInformation>();
        
        }
    }
}