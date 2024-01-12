
using MediatR;
using ProductService.Application.Contract.Persistant;
using ProductService.Domain.Entity;

namespace ProductService.Application.Feature.DiscountFeature.Query.GetDiscount
{
    public class GetDiscountCommandHandler(
        IDiscountRepository discountRepository
    ) : IRequestHandler<GetDiscountCommand, Discount>
    {
        private readonly IDiscountRepository _discountRepository = discountRepository;

        public async Task<Discount> Handle(GetDiscountCommand request, CancellationToken cancellationToken)
        {
            return await _discountRepository.GetByIdAsync(request.DiscountId);
        }
    }
}