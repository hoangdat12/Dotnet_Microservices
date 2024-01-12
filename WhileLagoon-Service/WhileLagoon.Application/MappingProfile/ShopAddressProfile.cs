using AutoMapper;
using WhileLagoon.Application.Feature.ShopAddressFeature.Command.CreateShopAddress;
using WhileLagoon.Domain.Entity;

namespace WhileLagoon.Application.MappingProfile
{
    public class ShopAddressProfile: Profile
    {
        public ShopAddressProfile() {
            CreateMap<CreateShopAddressCommand, ShopAddress>();
        } 
    }
}
