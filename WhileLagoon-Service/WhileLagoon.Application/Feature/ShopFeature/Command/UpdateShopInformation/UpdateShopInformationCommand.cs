using MediatR;
using WhileLagoon.Application.Dto.Shop;
using WhileLagoon.Domain.Entity;

namespace WhileLagoon.Application.Feature.ShopFeature.Command.UpdateShopInformation
{
    public record UpdateShopInformationCommand: IRequest<Shop>
    {
        public UpdateShopInfor UpdateShopInfor { get; set; }
        public User User { get; set; }
    }
}
