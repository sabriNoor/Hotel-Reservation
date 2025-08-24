using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using HotelReservation.Application.DTOs.Common;

namespace HotelReservation.Application.Validation.Common
{
    public class MoneyDtoValidator : AbstractValidator<MoneyDto>
    {
        public MoneyDtoValidator()
        {
            RuleFor(x => x.Amount)
            .GreaterThanOrEqualTo(0).WithMessage("Amount must be at least 0.");
            RuleFor(x => x.Currency)
            .NotEmpty().WithMessage("Currency is required.")
            .Length(3).WithMessage("Currency must be a 3-letter code.");
           
           
        }
    }
}