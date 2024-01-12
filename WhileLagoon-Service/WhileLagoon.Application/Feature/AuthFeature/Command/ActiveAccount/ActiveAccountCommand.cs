using MediatR;

namespace WhileLagoon.Application.Feature.AuthFeature.Command.ActiveAccount
{
    public record ActiveAccountCommand(string Token): IRequest<Unit>
    {
    }
}