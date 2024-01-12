using UserGRPCService;

namespace ProductService.Application.Contract.Infrastructure
{
    public interface IUserGRPCClient
    {
        public Task<GetUserRes> GetUserAsync(string UserId);
        public Task<VerifyAccessTokenRes> VerifyAccessTokenAsync(string UserId, string Token);
    }
}