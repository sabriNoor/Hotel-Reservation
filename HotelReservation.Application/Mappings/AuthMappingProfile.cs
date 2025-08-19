using AutoMapper;
using HotelReservation.Application.DTOs.Auth;
using HotelReservation.Application.DTOs.Common;
using HotelReservation.Domain.Entities;
using HotelReservation.Domain.ValueObjects;

namespace HotelReservation.Application.Mappings
{
    public class AuthMappingProfile : Profile
    {
        public AuthMappingProfile()
        {
            CreateMap<RegisterRequestDto, User>()
                .ForMember(dest => dest.PasswordHash,
                    opt => opt.MapFrom(src => src.Password))
                .ForMember(dest => dest.PersonalInformation, opt => opt.MapFrom(src => src.PersonalInformation)
            );

            CreateMap<User, RegisterResponseDto>()
                .ForMember(dest => dest.UserId,
                    opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.FullName,
                    opt => opt.MapFrom(src => $"{src.PersonalInformation.FirstName} {src.PersonalInformation.LastName}")
            );

            CreateMap<PersonalInformationDto, PersonalInformation>();
               
               
        }
    }
}