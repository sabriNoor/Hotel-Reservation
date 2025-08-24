using AutoMapper;
using HotelReservation.Application.DTOs.Booking;
using HotelReservation.Domain.Entities;

namespace HotelReservation.Application.Mappings
{
    public class BookingMappingProfile : Profile
    {
        public BookingMappingProfile()
        {
            CreateMap<BookingCreateRequestDto, Booking>();
            CreateMap<Booking, BookingResponseDto>()
            .ForMember(dst => dst.TotalPrice,
                opt => opt.MapFrom(src => src.TotalPrice));

            CreateMap<Booking, BookingDetailResponseDto>()
             .ForMember(dst => dst.BookingId,
                opt => opt.MapFrom(src => src.Id))
            .ForMember(dst => dst.Stay,
                opt => opt.MapFrom(src => src.Stay))
            .ForMember(dst => dst.FullName,
            opt => opt.MapFrom(src => $"{src.User.PersonalInformation.FirstName} {src.User.PersonalInformation.LastName}"))
            .ForMember(dst => dst.Email,
                opt => opt.MapFrom(src => src.User.Email))
            .ForMember(dst => dst.TotalPrice,
                opt => opt.MapFrom(src => src.TotalPrice))
            .ForMember(dst => dst.RoomNumber,
                opt => opt.MapFrom(src => src.Room.Number));
            
            
        }

    }
}