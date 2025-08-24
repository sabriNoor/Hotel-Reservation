
using FluentValidation;
using HotelReservation.Application.DTOs.Review;

namespace HotelReservation.Application.Validation.Review
{
    public class ReviewCURequestDtoValidator : AbstractValidator<ReviewCURequestDto>
    {
        public ReviewCURequestDtoValidator()
        {
            RuleFor(x => x.Rating)
            .InclusiveBetween(1, 5)
            .WithMessage("Rating must be between 1 and 5.");
        }
    }
}