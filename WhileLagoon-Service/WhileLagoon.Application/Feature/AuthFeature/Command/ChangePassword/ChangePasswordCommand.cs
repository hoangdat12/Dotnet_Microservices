using MediatR;
using System.ComponentModel.DataAnnotations;


namespace WhileLagoon.Application.Feature.AuthFeature.Command.ChangePassword
{
    public record ChangePasswordCommand: IRequest<Unit>
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string CurPassword { get; set; }
        [Required]
        public string NewPassword { get; set; }
    }
}
