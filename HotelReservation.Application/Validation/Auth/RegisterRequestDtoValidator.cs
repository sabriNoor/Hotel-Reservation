using FluentValidation;
using HotelReservation.Application.DTOs.Auth;

namespace HotelReservation.Application.Validation.Auth
{
    public class RegisterRequestDtoValidator : AbstractValidator<RegisterRequestDto>
    {
        public RegisterRequestDtoValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Username is required.")
                .MinimumLength(3).WithMessage("Username must be at least 3 characters.")
                .MaximumLength(50).WithMessage("Username must not exceed 50 characters.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters.");

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty().WithMessage("Confirm Password is required.")
                .Equal(x => x.Password).WithMessage("Passwords do not match.");

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
