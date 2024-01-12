using ProductService.Application.Feature.ProductFeature.Command.CreateProduct;
using ProductService.Application.Feature.ProductFeature.Command.DeleteProduct;
using ProductService.Application.Feature.ProductFeature.Command.UpdateProduct;
using ProductService.Application.Response;
using ProductService.Domain.Entity;

namespace ProductService.Application.Contract.Infrastructure
{
    public interface IProductFactory
    {
        public Task<Product> CreateProduct(CreateProductCommand request);
        public Task<BaseResponse> UpdateProduct(UpdateProductCommand request);
        public Task<BaseResponse> DeleteProduct(DeleteProductCommand request);
    }
}