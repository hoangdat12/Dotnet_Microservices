

namespace WhileLagoon.Application.Contract.Service
{
    public interface IRedisService
    {
        Task SetStringAsync(string key, object value, TimeSpan timeOut);
        Task<string> GetStringAsync(string key);
        Task<string> GetStringNoneReplaceAsync(string key);
        Task DeleteStringAsync(string key);
        Task SetStringCapitalAsync(string key, object value, TimeSpan timeOut);
        void DeleteString(string key);
    }
}
