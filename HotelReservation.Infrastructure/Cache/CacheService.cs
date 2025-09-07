using System.Text.Json;
using HotelReservation.Application.ICache;
using Microsoft.Extensions.Caching.Distributed;

namespace HotelReservation.Infrastructure.Cache
{
    public class CacheService : ICacheService
    {
        private readonly IDistributedCache _cache;
        public CacheService(IDistributedCache cache)
        {
            _cache = cache;

        }

        public async Task<bool> ExistsAsync(string key)
        {
            var cached = await _cache.GetAsync(key);
            return cached != null;
        }


        public async Task<T?> GetAsync<T>(string key)
        {
            var cached = await _cache.GetStringAsync(key);

            if (!string.IsNullOrEmpty(cached))
                return JsonSerializer.Deserialize<T>(cached);
            return default;
        }

        public async Task RemoveAsync(string key)
        {
            await _cache.RemoveAsync(key);
        }

        public async Task SetAsync<T>(string key, T value, TimeSpan? expiry = null)
        {
            var serialized = JsonSerializer.Serialize(value);
            await _cache.SetStringAsync(key, serialized,
                     new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = expiry });

        }
    }
}