using AutoMapper;
using HotelReservation.Application.DTOs.Review;
using HotelReservation.Domain.Entities;

namespace HotelReservation.Application.Mappings
{
    public class ReviewMappingProfile : Profile
    {
        public ReviewMappingProfile()
        {
            CreateMap<ReviewCURequestDto, Review>().ReverseMap();
            CreateMap<Review, ReviewResponseDto>().ReverseMap();

            CreateMap<Review, ReviewDetailDto>()
            .ForMember(x => x.UserEmail,
                opt => opt.MapFrom(src => src.Booking != null ? src.Booking.User.Email : null))
            .ForMember(x => x.Username,
                opt => opt.MapFrom(src => src.Booking != null ? src.Booking.User.Username : null))
            .ForMember(x => x.UserFullName,
                opt => opt.MapFrom(src =>
                    src.Booking != null && src.Booking.User.PersonalInformation != null
                        ? $"{src.Booking.User.PersonalInformation.FirstName} {src.Booking.User.PersonalInformation.LastName}"
                        : string.Empty));

        }
        
    }
}