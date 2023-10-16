using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Mogym.Application.Interfaces.ICache;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Mogym.Application.Services.Cache
{
    public class RedisCacheService:IRedisCacheService
    {
        private readonly IDistributedCache _cache;

        public RedisCacheService(IDistributedCache cache)
        {
            _cache = cache;
        }
        public async Task<T?> Get<T>(string key)
        {
            var value = await _cache.GetAsync(key);

            if (value != null)
            {
                var cachedString = Encoding.UTF8.GetString(value);
                if (cachedString != "Newtonsoft.Json.JsonSerializer")
                {
                    return JsonSerializer.Deserialize<T>(cachedString);
                }
            }

            return default(T);
        }

        public async Task<T?> Set<T>(string key, T value)
        {
            await Reset(key);

            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(180),
                SlidingExpiration = TimeSpan.FromMinutes(10)
            };

            var cachedString = JsonSerializer.Serialize(value);
            var newDataToCache = Encoding.UTF8.GetBytes(cachedString);

            await _cache.SetAsync(key, newDataToCache, options);

            return default;
        }

        public async Task<T?> Set<T>(string key, T value, int absoluteExpirationMinutes)
        {
            await Reset(key);

            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(absoluteExpirationMinutes)
            };

            var cachedString = JsonConvert.SerializeObject(value);

            var newDataToCache = Encoding.UTF8.GetBytes(cachedString);

            await _cache.SetAsync(key, newDataToCache, options);



            return default;
        }

        public async Task<T?> Set<T>(string key, T value, int absoluteExpirationMinutes, int slidingExpirationMinutes)
        {
            await Reset(key);

            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(absoluteExpirationMinutes),
                SlidingExpiration = TimeSpan.FromMinutes(slidingExpirationMinutes)
            };

            var cachedString = JsonConvert.SerializeObject(value);
            var newDataToCache = Encoding.UTF8.GetBytes(cachedString);

            await _cache.SetAsync(key, newDataToCache, options);

            return default;
        }


        public async Task Reset(string key)
        {
            await _cache.RemoveAsync(key);
        }
    }
}
