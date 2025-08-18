using System.Linq.Expressions;
using HotelReservation.Application.IRepository;
using HotelReservation.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HotelReservation.Infrastructure.Persistence.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        protected readonly AppDbContext _dbcontext;
        protected readonly DbSet<T> _dbSet;
        public GenericRepository(AppDbContext dbcontext)
        {
            _dbcontext = dbcontext;
            _dbSet = _dbcontext.Set<T>();

        }
        public async Task AddAsync(T Entity,CancellationToken ct=default)
        {
            await _dbSet.AddAsync(Entity,ct);
        }

        public async Task DeleteAsync(Guid Id,CancellationToken ct=default)
        {
            T? entity = await _dbSet.FindAsync(Id,ct);
            if (entity != null)
            {
                _dbSet.Remove(entity);
            }
        }

        public async Task<bool> ExistsAsync(Guid id,CancellationToken ct=default)
        {
            return await _dbSet.AnyAsync(e => e.Id == id,ct);
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate,CancellationToken ct=default)
        {
            return await _dbSet.Where(predicate).ToListAsync(ct);
        }

        public async Task<IEnumerable<T>> GetAllAsync(CancellationToken ct=default)
        {
            return await _dbSet.ToListAsync(ct);
        }

        public async Task<T?> GetByIdAsync(Guid Id,CancellationToken ct=default)
        {
            return await _dbSet.FirstOrDefaultAsync(e => e.Id == Id,ct);
        }


        public void Update(T Entity)
        {
            _dbSet.Update(Entity);
        }
    }
}