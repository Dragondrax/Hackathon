
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace MedicalHealth.Fiap.Data.CacheService
{
    public class CacheService : ICacheService
    {
        private readonly IDistributedCache _cache;
        private readonly DistributedCacheEntryOptions _defaultOptions;

        public CacheService(IDistributedCache cache)
        {
            _cache = cache;
            _defaultOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(60)
            };
        }
        public async Task<T?> GetAsync<T>(string key)
        {
            var data = await _cache.GetStringAsync(key);
            return data is not null ? JsonConvert.DeserializeObject<T>(data) : default;
        }

        public async Task SetAsync<T>(string key, T value, TimeSpan? expiration = null)
        {
            var options = expiration.HasValue ? new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = expiration
            } : _defaultOptions;

            var serializedData = JsonConvert.SerializeObject(value);
            await _cache.SetStringAsync(key, serializedData, options);
        }
        public async Task RemoveAsync(string key)
        {
            await _cache.RemoveAsync(key);
        }
    }
}
