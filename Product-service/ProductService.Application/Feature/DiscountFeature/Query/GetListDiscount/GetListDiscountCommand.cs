

using MediatR;
using ProductService.Application.Dto;
using ProductService.Domain.Entity;

namespace ProductService.Application.Feature.DiscountFeature.Query.GetListDiscount
{
    public record GetListDiscountCommand(Pagination Pagination): IRequest<List<Discount>>
    {
        
    }
}