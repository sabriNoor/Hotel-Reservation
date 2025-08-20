
using FluentValidation;
using HotelReservation.Application.DTOs.Room;

namespace HotelReservation.Application.Validation.Room
{
    public class RoomFilterDtoValidator : AbstractValidator<RoomFilterDto>
    {
        public RoomFilterDtoValidator()
        {
            RuleFor(x => x.StartDate)
                .NotEmpty().WithMessage("Start date is required.");

            RuleFor(x => x.EndDate)
                .NotEmpty().WithMessage("End date is required.");

            RuleFor(x => x.StartDate)
                .LessThan(x => x.EndDate)
                .WithMessage("Start date must be earlier than end date.");
        }
    }
}