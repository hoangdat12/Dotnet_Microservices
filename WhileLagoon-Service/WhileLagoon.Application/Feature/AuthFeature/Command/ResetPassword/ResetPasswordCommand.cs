using MediatR;
using WhileLagoon.Application.Response;

namespace WhileLagoon.Application.Feature.AuthFeature.Command.ResetPassword
{
    public record ResetPasswordCommand: IRequest<BaseResponse>
    {
        public string Token {get; set;}
        public string Password {get; set;}
    }
}