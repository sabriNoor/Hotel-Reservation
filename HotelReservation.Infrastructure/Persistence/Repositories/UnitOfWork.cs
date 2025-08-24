using HotelReservation.Application.IRepository;
using HotelReservation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace HotelReservation.Infrastructure.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly AppDbContext _dbContext;
        private bool _disposed;
        public IAmenityRepository AmenityRepository { get; }

        public IBookingRepository BookingRepository { get; }

        public IReviewRepository ReviewRepository { get; }

        public IRoomRepository RoomRepository { get; }
        public IUserRepository UserRepository { get; }

        public UnitOfWork(
            AppDbContext dbContext,
            IAmenityRepository amenityRepository,
            IBookingRepository bookingRepository,
            IReviewRepository reviewRepository,
            IRoomRepository roomRepository,
            IUserRepository userRepository
        )
        {
            _dbContext = dbContext;
            AmenityRepository = amenityRepository;
            BookingRepository = bookingRepository;
            ReviewRepository = reviewRepository;
            RoomRepository = roomRepository;
            UserRepository = userRepository;

        }

        public async Task<int> CompleteAsync(CancellationToken ct = default)
        {
            foreach (var entry in _dbContext.ChangeTracker.Entries<BaseEntity>())
            {
                if (entry.State == EntityState.Modified)
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
            }

            return await _dbContext.SaveChangesAsync(ct);
        }



        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
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