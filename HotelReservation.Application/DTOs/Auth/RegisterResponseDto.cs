using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelReservation.Application.DTOs.Auth
{
    public record class RegisterResponseDto
    {
        public Guid UserId { get; init; }       
        public string Username { get; init; } = string.Empty;  
        public string Email { get; init; }= string.Empty;  
        public string FullName { get; init; }= string.Empty;  
        public DateTime CreatedAt { get; init; }
    }
}