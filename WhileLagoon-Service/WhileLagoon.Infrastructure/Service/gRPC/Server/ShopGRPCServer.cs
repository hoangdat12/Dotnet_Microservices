using AutoMapper;
using Grpc.Core;
using ShopGRPCService;
using WhileLagoon.Application.Contract.Repository;
using WhileLagoon.Domain.Entity;

namespace WhileLagoon.Infrastructure.Service
{
    public class ShopGRPCServer
    (
        IShopRepository shopRepository,
        IMapper mapper
    ): ShopGRPC.ShopGRPCBase
    {
        private readonly IShopRepository _shopRepository = shopRepository;
        private readonly IMapper _mapper = mapper;

        public override async Task<GetShopRes> GetShop(GetShopReq request, ServerCallContext context)
        {
            Shop foundShop = await _shopRepository.GetByIdAsync(new Guid(request.ShopId));

            if (foundShop.Equals(null))
                return new GetShopRes();
        
            GetShopRes res = new()
            {
                Id = foundShop.Id.ToString(),
                ShopName = foundShop.ShopName,
                ShopAvatar = foundShop.ShopAvatar
            };
            res.ShopOwner.AddRange(foundShop.ShopOwner);
            return res;
        }
    }
}