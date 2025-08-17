using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelReservation.Domain.ValueObjects
{
    public class DateRange
    {
        [Required]
        public DateTime CheckIn { get; set; }

        [Required]
        public DateTime CheckOut { get; set; }

        public int GetNights() => (CheckOut - CheckIn).Days;
    }
}