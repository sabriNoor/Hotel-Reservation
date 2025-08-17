using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelReservation.Application.Exceptions
{
    public class NotFoundException : AppException
    {
        public NotFoundException(string name, object key)
            : base($"{name} with key '{key}' was not found.")
        {
        }
        
    }
}