using System.Linq.Expressions;
using System.Text.Json;
using HotelReservation.Application.IRepository;
using HotelReservation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;

namespace HotelReservation.Infrastructure.Persistence.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        protected readonly AppDbContext _dbcontext;
        protected readonly DbSet<T> _dbSet;
        protected readonly IDistributedCache _cache;

        public GenericRepository(AppDbContext dbcontext, IDistributedCache cache)
        {
            _dbcontext = dbcontext;
            _dbSet = _dbcontext.Set<T>();
            _cache = cache;
        }

        public async Task AddAsync(T Entity, CancellationToken ct = default)
        {
            await _dbSet.AddAsync(Entity, ct);
            await _cache.RemoveAsync($"{typeof(T).Name}_All", ct);
        }

        public async Task DeleteAsync(Guid Id, CancellationToken ct = default)
        {
            T? entity = await _dbSet.FindAsync(Id, ct);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _cache.RemoveAsync(GetCacheKey(Id), ct);
                await _cache.RemoveAsync($"{typeof(T).Name}_All", ct);
            }
        }

        public async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate, CancellationToken ct = default)
        {
            return await _dbSet.AnyAsync(predicate, ct);
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken ct = default)
        {
            return await _dbSet.Where(predicate).ToListAsync(ct);
        }

        public async Task<IEnumerable<T>> GetAllAsync(CancellationToken ct = default)
        {
            var cacheKey = $"{typeof(T).Name}_All";
            var cached = await _cache.GetStringAsync(cacheKey, ct);

            if (!string.IsNullOrEmpty(cached))
            {
                return JsonSerializer.Deserialize<IEnumerable<T>>(cached)!;
            }

            var all = await _dbSet.ToListAsync(ct);
            await _cache.SetStringAsync(cacheKey, JsonSerializer.Serialize(all), 
                new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10) }, ct);
            return all;
        }

        public async Task<T?> GetByIdAsync(Guid Id, CancellationToken ct = default)
        {
            var cacheKey = GetCacheKey(Id);
            var cached = await _cache.GetStringAsync(cacheKey, ct);

            if (!string.IsNullOrEmpty(cached))
                return JsonSerializer.Deserialize<T>(cached);

            var entity = await _dbSet.FirstOrDefaultAsync(e => e.Id == Id, ct);
            if (entity != null)
            {
                await _cache.SetStringAsync(cacheKey, JsonSerializer.Serialize(entity),
                    new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10) }, ct);
            }
            return entity;
        }

        public void Update(T Entity)
        {
            _dbSet.Update(Entity);
            _cache.RemoveAsync(GetCacheKey(Entity.Id)); 
            _cache.RemoveAsync($"{typeof(T).Name}_All");
        }

        public async Task<T?> FindOneAsync(Expression<Func<T, bool>> predicate, CancellationToken ct = default)
        {
            return await _dbSet.Where(predicate).FirstOrDefaultAsync(ct);
        }

        protected string GetCacheKey(Guid id) => $"{typeof(T).Name}_{id}";
    }
}
