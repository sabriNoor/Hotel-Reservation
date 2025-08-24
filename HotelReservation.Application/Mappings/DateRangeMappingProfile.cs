using AutoMapper;
using HotelReservation.Application.DTOs.Common;
using HotelReservation.Domain.ValueObjects;

namespace HotelReservation.Application.Mappings
{
    public class DateRangeMappingProfile :Profile
    {
        public DateRangeMappingProfile()
        {
            CreateMap<DateRangeDto, DateRange>().ReverseMap();
            
        }

    }
}