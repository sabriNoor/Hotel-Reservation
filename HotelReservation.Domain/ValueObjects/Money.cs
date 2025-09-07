using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HotelReservation.Domain.ValueObjects
{
    public class Money
    {
        [Range(0, double.MaxValue, ErrorMessage = "Amount must be at least 0.")]
        [Column(TypeName ="decimal(10,2)")]
        public decimal Amount { get; set; }
        [Required(ErrorMessage = "Currency is required.")]
        [StringLength(3, MinimumLength = 3, ErrorMessage = "Currency must be a 3-letter code.")]
        public string Currency { get; set; } = "USD";


    }
}