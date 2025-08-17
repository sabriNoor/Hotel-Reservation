using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelReservation.Application.Exceptions
{
    public class UnauthorizedAccessException : AppException
    {
        public UnauthorizedAccessException() 
            : base("User is not authorized to perform this action.") { }
        
    }
}