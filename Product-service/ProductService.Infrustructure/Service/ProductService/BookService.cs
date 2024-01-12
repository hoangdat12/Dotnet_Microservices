using AutoMapper;
using ProductService.Application.Contract.Infrastructure;
using ProductService.Application.Contract.Persistant;
using ProductService.Application.Contract.RabbitMq;
using ProductService.Application.Feature.ProductFeature.Command.CreateProduct;
using ProductService.Application.Feature.ProductFeature.Command.DeleteProduct;
using ProductService.Application.Feature.ProductFeature.Command.UpdateProduct;
using ProductService.Application.Response;
using ProductService.Domain.Entity;
using System.Text.Json;

namespace ProductService.Infrustructure.Service
{
    public class BookService(
        IProductRepository productRepository, 
        IMapper mapper,
        IBookRepository bookRepository,
        IShopGRPCClient shopGRPCClient,
        IRabbitMqClient rabbitMqClient
    ) : ProductBase(productRepository, mapper, shopGRPCClient, rabbitMqClient)
    {
        private readonly IBookRepository _bookRepository = bookRepository;
        public override async Task<Product> CreateProduct(CreateProductCommand request)
        {
            Product product = await Create(request);

            Book book = JsonSerializer.Deserialize<Book>(product.ProductAttributes);
            book.ProductId = product.Id;
            await _bookRepository.CreateAsync(book);

            return product;
        }

        public override async Task<BaseResponse> DeleteProduct(DeleteProductCommand request)
        {
            BaseResponse result = await Delete(request);
            if (result.IsSuccess) {
                await _bookRepository.DeleteByProductId(request.DeleteProductReq.Id);
            }
            else throw new Exception("Internal Server");
            return result;
        }

        public override async Task<BaseResponse> UpdateProduct(UpdateProductCommand request)
        {
            BaseResponse result = await Update(request);
            if (result.IsSuccess) {
                Book foundProduct = await _bookRepository.GetByIdAsync(request.UpdateProductReq.Id);
                if (request.UpdateProductReq.ProductAttributes != null) {
                    Book product = JsonSerializer.Deserialize<Book>(JsonSerializer.Serialize(request.UpdateProductReq.ProductAttributes));

                    foundProduct.Author = product.Author ?? foundProduct.Author;
                    foundProduct.Language = product.Language ?? foundProduct.Language;
                    foundProduct.PublicYear = product.PublicYear ?? foundProduct.PublicYear;

                    await _bookRepository.UpdateAsync(foundProduct);
                }
            }
            else throw new Exception("Internal Server");
            return result;
        }
    }
}
