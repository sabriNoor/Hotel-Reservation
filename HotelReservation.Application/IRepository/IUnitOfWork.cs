namespace HotelReservation.Application.IRepository
{
    public interface IUnitOfWork
    {
        IAmenityRepository Amenities { get; }
        IBookingRepository Bookings { get; }
        IReviewRepository Reviews { get; }
        IRoomRepository Rooms { get; }
        Task<int> CompleteAsync();
    }
}