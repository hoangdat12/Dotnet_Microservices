using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Caching.Distributed;
using ProductService.Application.Contract.Infrastructure;
using StackExchange.Redis;

namespace ProductService.Infrustructure.Service
{
    public class RedisService(
        IDistributedCache distributedCache,
        IConnectionMultiplexer multiplexer
    ) : IRedisService
    {
        private readonly IDistributedCache _distributed = distributedCache;
        private readonly IConnectionMultiplexer _multiplexer = multiplexer;
        public async Task<string> GetCacheAsync(string cacheKey)
        {
            if (string.IsNullOrEmpty(cacheKey))
                return null;
            
            string cacheResponse = await _distributed.GetStringAsync(cacheKey);
            return string.IsNullOrEmpty(cacheResponse) ? null : cacheResponse;
        }

        public async Task RemoveCacheWithPatternAsync(string pattern)
        {
            if (string.IsNullOrWhiteSpace(pattern)) 
                throw new Exception($"{nameof(pattern)} not be null or while space");

            foreach (var key in GetKey(pattern + "*"))
            {
                await _distributed.RemoveAsync(key);
            }
        }

        public async Task SetCacheAsync(string cacheKey, object response, TimeSpan timeOut)
        {
            if (response == null) return;

            var serializerResponse = JsonSerializer.Serialize(response, new JsonSerializerOptions
            {
                Converters = { new JsonStringEnumConverter() },
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            await _distributed.SetStringAsync(cacheKey, serializerResponse, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = timeOut
            });
        }

        private IEnumerable<string> GetKey(string pattern)
        {
            foreach (var endPoint in _multiplexer.GetEndPoints())
            {
                var server = _multiplexer.GetServer(endPoint);
                foreach (var key in server.Keys(pattern: pattern))
                {
                    yield return key.ToString();
                }
            }
        }
    }
}