
using MediatR;
using WhileLagoon.Application.Response;

namespace WhileLagoon.Application.Feature.AuthFeature.Command.ConfirmEmail
{
    public record ConfirmEmailCommand(string Token): IRequest<BaseResponse>
    {
        
    }
}