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