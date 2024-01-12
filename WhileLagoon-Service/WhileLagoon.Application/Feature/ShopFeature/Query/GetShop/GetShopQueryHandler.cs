using MediatR;
using WhileLagoon.Application.Contract.Repository;
using WhileLagoon.Domain.Entity;

namespace WhileLagoon.Application.Feature.ShopFeature.Query.GetShop
{
    public class GetShopQueryHandler(IShopRepository shopRepository) : IRequestHandler<GetShopQuery, Shop>
    {
        private readonly IShopRepository _shopRepository = shopRepository;
        public async Task<Shop> Handle(GetShopQuery request, CancellationToken cancellationToken)
        {
            return await _shopRepository.GetByIdAsync(request.ShopId);
        }
    }
}
