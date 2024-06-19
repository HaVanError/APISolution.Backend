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
        private readonly HashSet<string> _cachedKeys;
        public CacheServices(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
            _cachedKeys = new HashSet<string>();
        }
        public void Set<T>(string key, T value, TimeSpan expirationTime)
        {
            var cacheEntryOptions = new MemoryCacheEntryOptions
            {
              
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
            Remove(key); 
            Set(key, newValue, expirationTime); 
        }
        public bool Exists(string key)
        {
            return _memoryCache.TryGetValue(key, out _);
        }
        public List<T> GetList<T>(string key)
        {
            return Get<List<T>>(key);
        }
       
        public void SetList<T>(string key, List<T> value, TimeSpan expirationTime)
        {
           
            Set(key, value, expirationTime);
        }
       

    }
}

