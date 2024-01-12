
using MediatR;
using ProductService.Application.Dto;
using ProductService.Domain.Entity;

namespace ProductService.Application.Feature.DiscountFeature.Query.GetShopDiscount
{
    public record GetShopDiscountCommand(
        Pagination Pagination,
        Guid ShopId
    ): IRequest<List<Discount>>
    {
        
    }
}