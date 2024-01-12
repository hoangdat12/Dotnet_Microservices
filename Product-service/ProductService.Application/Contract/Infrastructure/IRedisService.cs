

namespace ProductService.Application.Contract.Infrastructure
{
    public interface IRedisService
    {
        Task SetCacheAsync(string cacheKey, object response, TimeSpan timeOut);
        Task<string> GetCacheAsync(string cacheKey);
        Task RemoveCacheWithPatternAsync(string pattern);
    }
}