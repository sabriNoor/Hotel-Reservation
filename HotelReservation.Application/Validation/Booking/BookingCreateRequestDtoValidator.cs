using FluentValidation;
using HotelReservation.Application.DTOs.Booking;

namespace HotelReservation.Application.Validation.Booking
{
    public class BookingCreateRequestDtoValidator : AbstractValidator<BookingCreateRequestDto>
    {
        public BookingCreateRequestDtoValidator()
        {
            RuleFor(x => x.NumberOfGuests)
            .GreaterThanOrEqualTo(1).WithMessage("Number of guests must be at least 1.");
            
        }
    }
}