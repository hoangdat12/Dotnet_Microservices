using MediatR;
using System.ComponentModel.DataAnnotations;
using WhileLagoon.Application.Dto.Auth;

namespace WhileLagoon.Application.Feature.AuthFeature.Command.Login
{
    public record LoginCommand: IRequest<AuthenticateRes>
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
