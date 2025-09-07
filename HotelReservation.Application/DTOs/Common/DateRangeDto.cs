using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelReservation.Application.DTOs.Common
{
    public record class DateRangeDto( DateTime CheckIn, DateTime CheckOut);
}