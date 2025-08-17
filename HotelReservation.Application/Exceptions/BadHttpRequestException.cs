using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelReservation.Application.Exceptions
{
    public class BadHttpRequestException : AppException
    {
        public BadHttpRequestException()
            : base("The request is invalid.")
        {
        }
    }
}