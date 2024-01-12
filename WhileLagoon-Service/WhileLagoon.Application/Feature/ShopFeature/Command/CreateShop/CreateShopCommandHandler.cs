using AutoMapper;
using MediatR;
using System.Text.Json;
using WhileLagoon.Application.Contract.Repository;
using WhileLagoon.Application.Contract.Service;
using WhileLagoon.Application.Exceptions;
using WhileLagoon.Domain.Constant;
using WhileLagoon.Domain.Entity;

namespace WhileLagoon.Application.Feature.ShopFeature.Command.CreateShop
{
    public class CreateShopCommandHandler(
        IShopRepository shopRepository,
        IShopAddressRepository shopAddressRepository,
        IRedisService redisService,
        IMapper mapper
    ) : IRequestHandler<CreateShopCommand, Shop>
    {
        private readonly IShopRepository _shopRepository = shopRepository;
        private readonly IShopAddressRepository _shopAddressRepository = shopAddressRepository;
        private readonly IRedisService _redisService = redisService;
        private readonly IMapper _mapper = mapper;
        public async Task<Shop> Handle(CreateShopCommand request, CancellationToken cancellationToken)
        {
            Shop foundShop = await _shopRepository.GetByShopName(request.CreateShopReq.ShopName);
            if (foundShop is not null)
            {
                throw new BadRequestException("Shop with this name has already existed!");
            }
            string key = $"{RedisConstant.FIRST_ADDRESS}{request.CreateShopReq.AddressId}";
            string shopAddressString = await _redisService.GetStringNoneReplaceAsync(key);
            ShopAddress shopAddress = JsonSerializer.Deserialize<ShopAddress>( shopAddressString );

            Shop shop = _mapper.Map<Shop>(request.CreateShopReq);
            shop.Id = Guid.NewGuid();
            shop.ShopOwner.Add(request.User.Id.ToString());
           
            await _shopRepository.CreateAsync(shop);

            shopAddress.Shop = shop;
            shopAddress.ShopId = shop.Id;
            await _shopAddressRepository.CreateAsync(shopAddress);
            _redisService.DeleteString(key);

            return shop;
        }
    }
}
