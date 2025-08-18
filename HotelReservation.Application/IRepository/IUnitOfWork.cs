namespace HotelReservation.Application.IRepository
{
    public interface IUnitOfWork
    {
        IAmenityRepository AmenityRepository { get; }
        IBookingRepository BookingRepository { get; }
        IReviewRepository ReviewRepository { get; }
        IRoomRepository RoomRepository { get; }
        Task<int> CompleteAsync(CancellationToken ct = default);
    }
}