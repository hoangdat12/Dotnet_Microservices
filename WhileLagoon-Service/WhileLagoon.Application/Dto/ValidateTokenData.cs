using System.Security.Claims;
using WhileLagoon.Domain.Entity;

namespace WhileLagoon.Application.Dto
{
    public class ValidateTokenData
    {
        public User user;
        public ClaimsPrincipal principal;
    }
}