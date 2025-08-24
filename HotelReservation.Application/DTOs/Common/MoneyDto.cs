using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelReservation.Application.DTOs.Common
{
    public record MoneyDto(decimal Amount, string Currency = "USD");

}