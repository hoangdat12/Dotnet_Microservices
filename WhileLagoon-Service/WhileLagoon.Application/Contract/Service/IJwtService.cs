using System.Security.Claims;
using WhileLagoon.Domain.Entity;

namespace WhileLagoon.Application.Contract.Service
{
    public interface IJwtService
    {
        public string GenerateAccessToken(User user);
        public string GenerateRefreshToken(User user);
        public string RefreshToken(User user, string token);
        public Task<ClaimsPrincipal> VerifyAccessToken(Guid userId, string token);
        public Task<User> VerifyRefreshToken(Guid userId, string token);
    }
}
