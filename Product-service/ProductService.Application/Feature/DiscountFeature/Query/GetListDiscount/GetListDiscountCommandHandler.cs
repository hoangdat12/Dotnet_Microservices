

using MediatR;
using ProductService.Application.Contract.Persistant;
using ProductService.Domain.Entity;

namespace ProductService.Application.Feature.DiscountFeature.Query.GetListDiscount
{
    public class GetListDiscountCommandHandler
    (
        IDiscountRepository discountRepository
    ) : IRequestHandler<GetListDiscountCommand, List<Discount>>
    {
        private readonly IDiscountRepository _discountRepository = discountRepository;

        public async Task<List<Discount>> Handle(GetListDiscountCommand request, CancellationToken cancellationToken)
        {
            return await _discountRepository.GetListDiscountAsync(request.Pagination);
        }
    }
}