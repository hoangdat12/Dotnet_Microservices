using MediatR;
using WhileLagoon.Domain.Entity;

namespace WhileLagoon.Application.Feature.CartFeature.Query.GetCart
{
    public record GetCartQuery(Guid UserId, User User, int Page = 1, int Limit = 20): IRequest<Cart>
    {
        
    }
}