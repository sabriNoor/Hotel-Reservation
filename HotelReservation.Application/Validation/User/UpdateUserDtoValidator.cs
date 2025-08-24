using FluentValidation;
using HotelReservation.Application.DTOs.User;
using HotelReservation.Application.Validation.Common;

namespace HotelReservation.Application.Validation.User
{
    public class UpdateUserDtoValidator : AbstractValidator<UpdateUserDto>
    {
        public UpdateUserDtoValidator()
        {
            Include(new PersonalInformationDtoValidator());
        }
    }
}