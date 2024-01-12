using MediatR;
using System.ComponentModel.DataAnnotations;
using WhileLagoon.Domain.Entity;

namespace WhileLagoon.Application.Feature.AuthFeature.Command.Register
{
    public record RegisterCommand: IRequest<User>
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
    }
}
