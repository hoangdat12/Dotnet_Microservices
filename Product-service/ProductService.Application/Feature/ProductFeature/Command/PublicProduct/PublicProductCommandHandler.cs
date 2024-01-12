using MediatR;
using ProductService.Application.Contract.Infrastructure;
using ProductService.Application.Contract.Persistant;
using ProductService.Application.Exceptions;
using ProductService.Domain.Entity;

namespace ProductService.Application.Feature.ProductFeature.Command.PublicProduct
{
    public class PublicProductCommandHandler(
        IProductRepository productRepository,
        IRedisService redisService
    ) : IRequestHandler<PublicProductCommand, Product>
    {
        private readonly IProductRepository _productRepository = productRepository;
        private readonly IRedisService _redisService = redisService;
        public async Task<Product> Handle(PublicProductCommand request, CancellationToken cancellationToken)
        {
            Product foundProduct = await _productRepository.GetByIdAsync(request.Id) ?? throw new NotFoundException("Product not found!");

            foundProduct.IsPublished = true;
            foundProduct.IsDraft = false;

            // Check inventory if quantity = 0 then update quantity before public

            await _productRepository.UpdateAsync(foundProduct);

            // Clear cache
            await _redisService.RemoveCacheWithPatternAsync("/api/v1/Product");
            return foundProduct;
        }
    }
}