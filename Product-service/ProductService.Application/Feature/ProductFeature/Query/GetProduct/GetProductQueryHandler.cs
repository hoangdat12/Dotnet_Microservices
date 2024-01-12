using AutoMapper;
using MediatR;
using ProductService.Application.Contract.Persistant;
using ProductService.Application.Dto;
using ProductService.Domain.Entity;

namespace ProductService.Application.Feature.ProductFeature.Query.GetProduct
{
    public class GetProductQueryHandler(IMapper mapper, IProductRepository productRepository) : IRequestHandler<GetProductQuery, ProductDto>
    {
        private readonly IProductRepository _productRepository = productRepository;
        private readonly IMapper _mapper = mapper;
        public async Task<ProductDto> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            Product product = await _productRepository.GetByIdAsync(request.Id);

            ProductDto data = _mapper.Map<ProductDto>(product);
            return data;
        }
    }
}
