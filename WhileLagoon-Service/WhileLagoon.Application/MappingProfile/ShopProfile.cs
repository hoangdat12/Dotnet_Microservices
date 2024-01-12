using AutoMapper;
using ShopGRPCService;
using WhileLagoon.Application.Dto.Shop;
using WhileLagoon.Application.Feature.AuthFeature.Command.Register;
using WhileLagoon.Application.Feature.ShopFeature.Command.CreateShop;
using WhileLagoon.Domain.Entity;

namespace WhileLagoon.Application.MappingProfile
{
    public class ShopProfile: Profile
    {
        public ShopProfile()
        {
            CreateMap<CreateShopCommand, Shop>();
            CreateMap<CreateShopReq, Shop>();
            CreateMap<Shop, GetShopRes>();
        }
    }
}
