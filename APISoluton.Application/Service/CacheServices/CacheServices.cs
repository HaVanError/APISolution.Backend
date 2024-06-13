using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APISoluton.Application.Service.CacheServices
{
    public class CacheServices
    {
        private readonly IMemoryCache _memoryCache;
        public CacheServices(IMemoryCache memoryCache)
        {
        _memoryCache = memoryCache; 
        }
        public void Set<T>(string key, T value, TimeSpan expirationTime)
        {
            var cacheEntryOptions = new MemoryCacheEntryOptions
            {
               // AbsoluteExpirationRelativeToNow = expirationTime
               AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5),
                SlidingExpiration = TimeSpan.FromMinutes(2)
            };
            _memoryCache.Set(key, value, cacheEntryOptions);
        }

        public T Get<T>(string key)
        {
            return _memoryCache.TryGetValue(key, out T value) ? value : default(T);
        }

        public void Remove(string key)
        {
            _memoryCache.Remove(key);
        }
        public void Refresh<T>(string key, T newValue, TimeSpan expirationTime)
        {
            Remove(key); // Remove the old value
            Set(key, newValue, expirationTime); // Set the new value
        }
        public bool Exists(string key)
        {
            return _memoryCache.TryGetValue(key, out _);
        }
        public List<T> GetList<T>(string key)
        {
            return Get<List<T>>(key);
        }

        // Set list in cache
        public void SetList<T>(string key, List<T> value, TimeSpan expirationTime)
        {
            Set(key, value, expirationTime);
        }
    }
}

