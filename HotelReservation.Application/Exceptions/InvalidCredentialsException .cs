using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelReservation.Application.Exceptions
{
    public class InvalidCredentialsException : AppException
    {
        public InvalidCredentialsException() 
        : base("Invalid username or password")
        {
        }
    }
}