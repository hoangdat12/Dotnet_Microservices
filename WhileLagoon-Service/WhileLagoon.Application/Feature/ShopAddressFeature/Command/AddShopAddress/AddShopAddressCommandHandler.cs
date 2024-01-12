using AutoMapper;
using MediatR;
using WhileLagoon.Application.Contract.Repository;
using WhileLagoon.Application.Exceptions;
using WhileLagoon.Domain.Entity;

namespace WhileLagoon.Application.Feature.ShopAddressFeature.Command.AddShopAddress
{
    public class AddShopAddressCommandHandler
    (
        IShopAddressRepository shopAddressRepository,
        IShopRepository shopRepository,
        IMapper mapper
    ): IRequestHandler<AddShopAddressCommand, ShopAddress>
    {
        private readonly IShopAddressRepository _shopAddressRepository = shopAddressRepository;
        private readonly IShopRepository _shopRepository = shopRepository;
        private readonly IMapper _mapper = mapper;
        public async Task<ShopAddress> Handle(AddShopAddressCommand request, CancellationToken cancellationToken)
        {
            Shop foundShop = await _shopRepository.GetByIdAsync( request.ShopId )
                ?? throw new NotFoundException("Shop not found!");

            ShopAddress shopAddress = _mapper.Map<ShopAddress>( request.ShopAddress );

            shopAddress.ShopId = foundShop.Id;
            shopAddress.Shop = foundShop;

            await _shopAddressRepository.CreateAsync(shopAddress);

            return shopAddress;
        }
    }
}
