namespace HotelReservation.Application.Exceptions
{
    public class UnauthorizedAccessException : AppException
    {
        public UnauthorizedAccessException() 
            : base("User is not authorized to perform this action.") { }
        
    }
}