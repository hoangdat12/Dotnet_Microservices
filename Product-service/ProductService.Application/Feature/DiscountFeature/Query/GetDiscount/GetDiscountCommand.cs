
using MediatR;
using ProductService.Domain.Entity;

namespace ProductService.Application.Feature.DiscountFeature.Query
{
    public record GetDiscountCommand(Guid DiscountId): IRequest<Discount>
    {
        
    }
}