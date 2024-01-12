using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;
using System.Text.Json.Serialization;
using System.Text.Json;
using WhileLagoon.Application.Contract.Service;

namespace WhileLagoon.Infrastructure.Service
{
    public class RedisService(
        IDistributedCache distributedCache,
        IConnectionMultiplexer multiplexer
    ) : IRedisService
    {
        private readonly IDistributedCache _distributed = distributedCache;
        private readonly IConnectionMultiplexer _multiplexer = multiplexer;

        public void DeleteString(string key)
        {
            _distributed.Remove(key);
        }

        public async Task DeleteStringAsync(string key)
        {
            await _distributed.RemoveAsync(key);
        }

        public async Task<string> GetStringAsync(string key)
        {
            if (string.IsNullOrEmpty(key))
                return null;

            string cacheResponse = await _distributed.GetStringAsync(key);
            return string.IsNullOrEmpty(cacheResponse) ? null : cacheResponse.Replace("\"", "");
        }

        public async Task<string> GetStringNoneReplaceAsync(string key)
        {
            if (string.IsNullOrEmpty(key))
                return null;

            string cacheResponse = await _distributed.GetStringAsync(key);
            return string.IsNullOrEmpty(cacheResponse) ? null : cacheResponse;
        }

        public async Task SetStringAsync(string key, object value, TimeSpan timeOut)
        {
            if (value == null) return;

            var serializerResponse = JsonSerializer.Serialize(value, new JsonSerializerOptions
            {
                Converters = { new JsonStringEnumConverter() },
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            await _distributed.SetStringAsync(key, serializerResponse.ToString(), new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = timeOut
            });
        }

        public async Task SetStringCapitalAsync(string key, object value, TimeSpan timeOut)
        {
            if (value == null) return;

            var serializerResponse = JsonSerializer.Serialize(value);

            await _distributed.SetStringAsync(key, serializerResponse, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = timeOut
            });
        }
    }
}
