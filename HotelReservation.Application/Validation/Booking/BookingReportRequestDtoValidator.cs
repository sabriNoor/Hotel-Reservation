using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using HotelReservation.Application.DTOs.Booking;

namespace HotelReservation.Application.Validation.Booking
{
    public class BookingReportRequestDtoValidator : AbstractValidator<BookingReportRequestDto>
    {
        public BookingReportRequestDtoValidator()
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