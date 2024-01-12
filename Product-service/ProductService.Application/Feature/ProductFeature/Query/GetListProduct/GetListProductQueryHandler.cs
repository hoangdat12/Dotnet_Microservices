using AutoMapper;
using MediatR;
using ProductService.Application.Contract.Persistant;
using ProductService.Application.Dto;
using ProductService.Domain.Entity;

namespace ProductService.Application.Feature.ProductFeature.Query.GetListProduct
{
    public class GetListProductQueryHandler(
        IProductRepository productRepository,
        IMapper mapper
    ) : IRequestHandler<GetListProductQuery, List<ProductDto>>
    {
        private readonly IProductRepository _productRepository = productRepository;
        private readonly IMapper _mapper = mapper;
        public async Task<List<ProductDto>> Handle(GetListProductQuery request, CancellationToken cancellationToken)
        {
            List<Product> products = (List<Product>)await _productRepository.GetAsync(request.Pagination);
            List<ProductDto> productDtos = _mapper.Map<List<ProductDto>>(products);

            return productDtos;
        }
    }
}