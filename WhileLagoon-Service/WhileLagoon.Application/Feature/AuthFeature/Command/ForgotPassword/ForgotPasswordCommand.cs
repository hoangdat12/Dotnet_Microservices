using MediatR;
using WhileLagoon.Application.Response;

namespace WhileLagoon.Application.Feature.AuthFeature.Command.ForgotPassword
{
    public record ForgotPasswordCommand: IRequest<BaseResponse>
    {
        public string Email {get; set;}
    }
}