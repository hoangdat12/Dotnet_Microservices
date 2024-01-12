using ProductService.Application.Contract.Infrastructure;
using ProductService.Application.Feature.ProductFeature.Command.CreateProduct;
using ProductService.Application.Feature.ProductFeature.Command.DeleteProduct;
using ProductService.Application.Feature.ProductFeature.Command.UpdateProduct;
using ProductService.Application.Response;
using ProductService.Domain.Entity;

namespace ProductService.Infrustructure.Service
{
    public class ProductFactory(
        BookService bookService,
        ClothingService clothingService
    ) : IProductFactory
    {
        private readonly BookService _bookService = bookService;
        private readonly ClothingService _clothingService = clothingService;
        public Task<Product> CreateProduct(CreateProductCommand request)
        {
            return request.CreateProductReq.ProductType switch
            {
                ProductType.BOOK => _bookService.CreateProduct(request),
                ProductType.CLOTHING => _clothingService.CreateProduct(request),
                ProductType.ELECTRONIC => _bookService.CreateProduct(request),
                ProductType.FURNITURE => _bookService.CreateProduct(request),
                _ => throw new Exception("Type not found!"),
            };
        }

        public Task<BaseResponse> DeleteProduct(DeleteProductCommand request)
        {
            return request.DeleteProductReq.ProductType switch
            {
                ProductType.BOOK => _bookService.DeleteProduct(request),
                ProductType.CLOTHING => _clothingService.DeleteProduct(request),
                ProductType.ELECTRONIC => _bookService.DeleteProduct(request),
                ProductType.FURNITURE => _bookService.DeleteProduct(request),
                _ => throw new Exception("Type not found!"),
            };
        }

        public Task<BaseResponse> UpdateProduct(UpdateProductCommand request)
        {
            return request.UpdateProductReq.ProductType switch
            {
                ProductType.BOOK => _bookService.UpdateProduct(request),
                ProductType.CLOTHING => _clothingService.UpdateProduct(request),
                ProductType.ELECTRONIC => _bookService.UpdateProduct(request),
                ProductType.FURNITURE => _bookService.UpdateProduct(request),
                _ => throw new Exception("Type not found!"),
            };
        }
    }
}