using WhileLagoon.Domain.Entity;

namespace WhileLagoon.Application.Dto.Auth
{
    public record AuthenticateRes
    {
        public User User { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
