using MediatR;
using WhileLagoon.Domain.Entity;

namespace WhileLagoon.Application.Feature.ShopFeature.Query.GetShop
{
    public record GetShopQuery(Guid ShopId) : IRequest<Shop>
    {
    }
}
