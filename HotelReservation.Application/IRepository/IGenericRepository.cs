using System.Linq.Expressions;

namespace HotelReservation.Application.IRepository
{
    public interface IGenericRepository<T>
    {
        Task AddAsync(T Entity, CancellationToken ct = default);
        void Update(T Entity);
        Task DeleteAsync(Guid Id, CancellationToken ct = default);
        Task<IEnumerable<T>> GetAllAsync(CancellationToken ct = default);
        Task<T?> GetByIdAsync(Guid Id, CancellationToken ct = default);
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken ct = default);
        Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate, CancellationToken ct = default);
        Task<T?> FindOneAsync(Expression<Func<T, bool>> predicate, CancellationToken ct = default);
    }
}