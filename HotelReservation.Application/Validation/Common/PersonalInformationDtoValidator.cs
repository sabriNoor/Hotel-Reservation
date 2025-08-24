using FluentValidation;
using HotelReservation.Application.DTOs.Common;

namespace HotelReservation.Application.Validation.Common
{
    public class PersonalInformationDtoValidator : AbstractValidator<PersonalInformationDto>
    {
        public PersonalInformationDtoValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First Name is required.")
                .MinimumLength(3).WithMessage("First Name must be at least 3 characters.")
                .MaximumLength(50).WithMessage("First Name must not exceed 50 characters.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last Name is required.")
                .MinimumLength(3).WithMessage("Last Name must be at least 3 characters.")
                .MaximumLength(50).WithMessage("Last Name must not exceed 50 characters.");

            RuleFor(x => x.MobileNumber)
                .NotEmpty().WithMessage("Mobile Number is required.")
                .Matches(@"^[0-9]{10}$").WithMessage("Invalid Mobile Number. Must be 10 digits.");
        }
        
    }
}