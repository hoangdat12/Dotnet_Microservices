using MediatR;
using WhileLagoon.Application.Response;
using WhileLagoon.Domain.Entity;

namespace WhileLagoon.Application.Feature.ShopFeature.Command.DeleteShop
{
    public record DeleteShopCommand : IRequest<BaseResponse>
    {
        public User User { get; set; }
        public Guid ShopId { get; set; }
    }
}
