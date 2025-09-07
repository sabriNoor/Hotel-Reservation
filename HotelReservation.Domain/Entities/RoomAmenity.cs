using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelReservation.Domain.Entities
{
    public class RoomAmenity
    {
        public Guid RoomId { get; set; }
        public Room Room { get; set; } = null!;
        public Guid AmenityId { get; set; }
        public Amenity Amenity { get; set; } = null!;
    }
}