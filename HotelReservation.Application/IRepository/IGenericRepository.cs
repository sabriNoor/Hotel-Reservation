using System.Linq.Expressions;

namespace HotelReservation.Application.IRepository
{
    public interface IGenericRepository<T>
    {
        Task AddAsync(T Entity);
        Task UpdateAsync(T Entity);
        Task DeleteAsync(object Id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(object Id);
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
        Task<bool> ExistsAsync(object id);
    }
}