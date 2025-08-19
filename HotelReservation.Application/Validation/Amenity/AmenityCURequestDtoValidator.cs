using FluentValidation;
using HotelReservation.Application.DTOs.Amenity;

namespace HotelReservation.Application.Validation.Amenity
{
    public class CURequestAmenityDtoValidator : AbstractValidator<AmenityCURequestDto>
    {
        public CURequestAmenityDtoValidator()
        {
            RuleFor(x => x.Name)
               .NotEmpty().WithMessage("Name is required.")
               .MinimumLength(3).WithMessage("Name must be at least 3 characters.")
               .MaximumLength(50).WithMessage("Name must not exceed 50 characters.");
           
            

        }
    }
}