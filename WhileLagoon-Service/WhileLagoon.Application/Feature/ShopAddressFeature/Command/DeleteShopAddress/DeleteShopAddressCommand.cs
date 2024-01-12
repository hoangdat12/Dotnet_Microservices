using WhileLagoon.Domain.Entity;

namespace WhileLagoon.Application.Feature.ShopAddressFeature.Command.DeleteShopAddress
{
    public record DeleteShopAddressCommand
    {
        public Guid AddressId { get; set; }
        public Guid ShopId { get; set; }    
        public User User { get; set; }
    }
}
