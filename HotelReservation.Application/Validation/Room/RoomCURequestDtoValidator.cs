using FluentValidation;
using HotelReservation.Application.DTOs.Room;
using HotelReservation.Application.Validation.Common;

namespace HotelReservation.Application.Validation.Room
{
    public class RoomCURequestDtoValidator : AbstractValidator<RoomCURequestDto>
    {
        public RoomCURequestDtoValidator()
        {
            RuleFor(x => x.Capacity)
            .GreaterThanOrEqualTo(1).WithMessage("Capacity must be greater than or equal to 1.")
            .LessThanOrEqualTo(100).WithMessage("Capacity must be less than or equal to 100.");

            RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");

            RuleFor(x => x.IsAvailable);

            RuleFor(x => x.Number)
            .GreaterThan(0).WithMessage("Room number must be greater than 0.");

            RuleFor(x => x.PricePerNight)
            .NotNull().SetValidator(new MoneyDtoValidator());

            RuleFor(x => x.Type)
            .NotEmpty().WithMessage("Room type is required.");
            
            
        }
        
    }
}