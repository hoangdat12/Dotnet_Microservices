using MediatR;
using ProductService.Application.Contract.Infrastructure;
using ProductService.Application.Contract.Persistant;
using ProductService.Application.Exceptions;
using ProductService.Domain.Entity;

namespace ProductService.Application.Feature.ProductFeature.Command.DraftProduct
{
    public class DraftProductCommandHandler(
        IProductRepository productRepository,
        IRedisService redisService
    ) : IRequestHandler<DraftProductCommand, Product>
    {
        private readonly IProductRepository _productRepository = productRepository;
        private readonly IRedisService _redisService = redisService; 

        public async Task<Product> Handle(DraftProductCommand request, CancellationToken cancellationToken)
        {
            Product foundProduct = await _productRepository.GetByIdAsync(request.Id) 
                ?? throw new NotFoundException("Product not found!");
            foundProduct.IsPublished = false;
            foundProduct.IsDraft = true;
            await _productRepository.UpdateAsync(foundProduct);

            // Clear cache
            await _redisService.RemoveCacheWithPatternAsync("/api/v1/Product");

            return foundProduct;
        }
    }
}