using MediatR;
using System.ComponentModel.DataAnnotations;
using WhileLagoon.Application.Dto.Shop;
using WhileLagoon.Domain.Entity;

namespace WhileLagoon.Application.Feature.ShopFeature.Command.CreateShop
{
    public record CreateShopCommand: IRequest<Shop>
    {
        public CreateShopReq CreateShopReq { get; set; }
        public User User { get; set; }
    }
}
