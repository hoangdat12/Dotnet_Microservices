using MediatR;
using System.ComponentModel.DataAnnotations;
using WhileLagoon.Application.Feature.ShopFeature.Command.CreateShop;
using WhileLagoon.Domain.Entity;

namespace WhileLagoon.Application.Feature.ShopAddressFeature.Command.AddShopAddress
{
    public record AddShopAddressCommand: IRequest<ShopAddress>
    {
        [Required]
        public Guid ShopId { get; set; }
        [Required]
        public CreateShopCommand ShopAddress { get; set; }
    }
}
