using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelReservation.Domain.Entities
{
    public class Review : BaseEntity
    {
        public Guid BookingId { get; set; }
        public Booking Booking { get; set; } = null!;

        [Range(1, 5)]
        public int Rating { get; set; }
        
        public string? Comment { get; set; }
        
    }
}