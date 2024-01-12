using MediatR;
using ProductService.Application.Dto;

namespace ProductService.Application.Feature.ProductFeature.Query.GetProduct
{
    public record GetProductQuery(Guid Id) : IRequest<ProductDto>;
}
