using Microsoft.Extensions.Caching.Memory;

namespace LiteraFlow.Web.Services
{
    public interface ICacheService
    {
        void Set<T>(in string key, T value, int seconds = 60);

        T? GetOrNull<T>(in string key);

        Task<T?> GetOrCreateAsync<T>(string key, Func<ICacheEntry, Task<T>> factory);
      
        void Remove(in string key);

    }


    public class CacheService : ICacheService
    {
        private readonly IMemoryCache cache;

        public CacheService()
        {
            cache = new MemoryCache(new MemoryCacheOptions());
        }

        public async Task<T?> GetOrCreateAsync<T>(string key, Func<ICacheEntry, Task<T>> factory)
        {
            return await cache.GetOrCreateAsync(key, factory);
        }

        public T? GetOrNull<T>(in string key)
        {
            if (!cache.TryGetValue(key, out T? value)) 
                return default;
            return value;
        }

        public void Remove(in string key)
        {
            cache.Remove(key);
        }

        public void Set<T>(in string key, T value, int seconds = 60)
        {
            TimeSpan expirationTime = TimeSpan.FromSeconds(seconds);
            cache.Set(key, value, expirationTime);
        }
    }
}
