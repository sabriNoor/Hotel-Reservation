using HotelReservation.Application.IRepository;

namespace HotelReservation.Infrastructure.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly AppDbContext _dbContex;
        private bool _disposed;
        public IAmenityRepository AmenityRepository { get; }

        public IBookingRepository BookingRepository { get; }

        public IReviewRepository ReviewRepository { get; }

        public IRoomRepository RoomRepository { get; }
        public IUserRepository UserRepository { get; }

        public UnitOfWork(
            AppDbContext dbContex,
            IAmenityRepository amenityRepository,
            IBookingRepository bookingRepository,
            IReviewRepository reviewRepository,
            IRoomRepository roomRepository,
            IUserRepository userRepository
        )
        {
            _dbContex = dbContex;
            AmenityRepository = amenityRepository;
            BookingRepository = bookingRepository;
            ReviewRepository = reviewRepository;
            RoomRepository = roomRepository;
            UserRepository = userRepository;

        }

        public async Task<int> CompleteAsync(CancellationToken ct = default)
        {
            return await _dbContex.SaveChangesAsync(ct);
        }

        
        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _dbContex.Dispose();
                }
            }
            _disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}