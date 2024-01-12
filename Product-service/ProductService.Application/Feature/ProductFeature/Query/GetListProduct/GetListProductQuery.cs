using MediatR;
using ProductService.Application.Dto;

namespace ProductService.Application.Feature.ProductFeature.Query.GetListProduct
{
    public record GetListProductQuery (
        Pagination Pagination
    )
    : IRequest<List<ProductDto>>
    {
    }
}