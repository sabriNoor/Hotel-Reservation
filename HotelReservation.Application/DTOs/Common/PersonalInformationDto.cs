using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelReservation.Application.DTOs.Common
{
    public record class PersonalInformationDto
    {
        public string FirstName { get; init; } = string.Empty;
        public string LastName { get; init; } = string.Empty;
        public string MobileNumber { get; init; } = string.Empty;
    }
}