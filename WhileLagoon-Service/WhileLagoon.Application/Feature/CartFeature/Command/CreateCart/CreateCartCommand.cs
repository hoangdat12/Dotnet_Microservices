
using MediatR;
using WhileLagoon.Domain.Entity;

namespace WhileLagoon.Application.Feature.CartFeature.Command.CreateCart
{
    public record CreateCartCommand: IRequest<Cart>
    {
        public Guid UserId {get; set;}
    }
}