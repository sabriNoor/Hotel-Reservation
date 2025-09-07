using FluentValidation;
using HotelReservation.Application.DTOs.Common;

namespace HotelReservation.Application.Validation.Common
{
    public class DateRangeDtoValidator : AbstractValidator<DateRangeDto>
    {
        public DateRangeDtoValidator()
        {
            RuleFor(x => x.CheckIn)
                .NotEmpty().WithMessage("Check-in is required.");

            RuleFor(x => x.CheckOut)
                .NotEmpty().WithMessage("Check-out is required.");

            RuleFor(x => x.CheckIn)
                .LessThan(x => x.CheckOut)
                .WithMessage("Check-in must be earlier than Check-out.");
            
        }
        
    }
}