using AutoMapper;
using HotelReservation.Application.DTOs.Common;
using HotelReservation.Domain.ValueObjects;

namespace HotelReservation.Application.Mappings
{
    public class MoneyMappingProfile : Profile
    {
        public MoneyMappingProfile()
        {
            CreateMap<MoneyDto, Money>().ReverseMap();
        }
    }
}