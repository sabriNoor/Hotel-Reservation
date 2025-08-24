using FluentValidation;
using HotelReservation.Application.DTOs.Booking;

namespace HotelReservation.Application.Validation.Booking
{
    public class BookingStatusUpdateRequestDtoValidator : AbstractValidator<BookingStatusUpdateRequestDto>
    {
        public BookingStatusUpdateRequestDtoValidator()
        {
            RuleFor(x => x.Status)
            .NotEmpty().WithMessage("Status is required.");
        }
    }
}