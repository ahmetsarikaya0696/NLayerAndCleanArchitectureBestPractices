using Application.Contracts.Caching;
using Microsoft.Extensions.Caching.Memory;

namespace Caching
{
    public class CacheService(IMemoryCache memoryCache) : ICacheService
    {
        public Task AddAsync<T>(string cacheKey, T value, TimeSpan expireTimeSpan)
        {
            var cacheOptions = new MemoryCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow = expireTimeSpan,
            };

            memoryCache.Set(cacheKey, value, cacheOptions);

            return Task.CompletedTask;
        }

        public Task<T?> GetAsync<T>(string cacheKey)
        {
            if (memoryCache.TryGetValue(cacheKey, out T? cacheItem))
            {
                return Task.FromResult(cacheItem);
            }

            return Task.FromResult(default(T));
        }

        public Task RemoveAsync(string cacheKey)
        {
            memoryCache.Remove(cacheKey);

            return Task.CompletedTask;
        }
    }
}
