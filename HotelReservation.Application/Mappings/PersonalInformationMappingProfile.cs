using AutoMapper;
using HotelReservation.Application.DTOs.Common;
using HotelReservation.Domain.ValueObjects;

namespace HotelReservation.Application.Mappings
{
    public class PersonalInformationMappingProfile : Profile
    {
        public PersonalInformationMappingProfile()
        {
            CreateMap<PersonalInformationDto, PersonalInformation>().ReverseMap();
        }
    }
}