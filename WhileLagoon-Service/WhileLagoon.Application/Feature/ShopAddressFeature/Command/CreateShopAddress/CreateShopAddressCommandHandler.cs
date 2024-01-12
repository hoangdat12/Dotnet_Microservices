using AutoMapper;
using MediatR;
using WhileLagoon.Application.Contract.Service;
using WhileLagoon.Domain.Constant;
using WhileLagoon.Domain.Entity;

namespace WhileLagoon.Application.Feature.ShopAddressFeature.Command.CreateShopAddress
{
    public class CreateShopAddressCommandHandler(
        IRedisService redisService,
        IMapper mapper
    ) : IRequestHandler<CreateShopAddressCommand, ShopAddress>
    {
        private readonly IRedisService _redisService = redisService;
        private readonly IMapper _mapper = mapper;
        public async Task<ShopAddress> Handle(CreateShopAddressCommand request, CancellationToken cancellationToken)
        {
            ShopAddress shopAddress = _mapper.Map<ShopAddress>( request );
            shopAddress.Id = Guid.NewGuid();
            string key = $"{RedisConstant.FIRST_ADDRESS}{shopAddress.Id}";
            TimeSpan expireIn = TimeSpan.FromMinutes(5);
            await _redisService.SetStringCapitalAsync(key, shopAddress, expireIn);

            return shopAddress;
        }
    }
}
