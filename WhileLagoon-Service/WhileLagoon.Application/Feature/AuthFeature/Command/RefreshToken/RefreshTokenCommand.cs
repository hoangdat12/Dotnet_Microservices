using System.ComponentModel.DataAnnotations;
using MediatR;

namespace WhileLagoon.Application.Feature.AuthFeature.Command.RefreshToken
{
    public record RefreshTokenCommand: IRequest<string>
    {
        [Required]
        public Guid UserId {get; set;}
        [Required]
        public string RefreshToken {get; set;}
    }
}