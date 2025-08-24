using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelReservation.Domain.Entities
{
    public class Amenity : BaseEntity
    {
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 50 characters.")]
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        
        public ICollection<RoomAmenity> RoomAmenities  { get; set; } = new List<RoomAmenity>();
    }
}