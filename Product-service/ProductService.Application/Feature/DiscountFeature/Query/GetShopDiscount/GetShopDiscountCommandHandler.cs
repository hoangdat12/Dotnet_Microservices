

using MediatR;
using ProductService.Application.Contract.Persistant;
using ProductService.Domain.Entity;

namespace ProductService.Application.Feature.DiscountFeature.Query.GetShopDiscount
{
    public class GetShopDiscountCommandHandler(
        IDiscountRepository discountRepository
    ) : IRequestHandler<GetShopDiscountCommand, List<Discount>>
    {
        private readonly IDiscountRepository _discountRepository = discountRepository;

        public async Task<List<Discount>> Handle(GetShopDiscountCommand request, CancellationToken cancellationToken)
        {
            return await _discountRepository.GetShopDiscountAsync(
                request.ShopId,
                request.Pagination    
            );
        }
    }
}