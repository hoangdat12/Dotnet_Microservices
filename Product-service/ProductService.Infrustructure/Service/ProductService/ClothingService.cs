
using System.Text.Json;
using AutoMapper;
using ProductService.Application.Contract.Infrastructure;
using ProductService.Application.Contract.Persistant;
using ProductService.Application.Contract.RabbitMq;
using ProductService.Application.Feature.ProductFeature.Command.CreateProduct;
using ProductService.Application.Feature.ProductFeature.Command.DeleteProduct;
using ProductService.Application.Feature.ProductFeature.Command.UpdateProduct;
using ProductService.Application.Response;
using ProductService.Domain.Entity;

namespace ProductService.Infrustructure.Service
{
    public class ClothingService
    (
        IProductRepository productRepository,
        IClothingRepository clothingRepository,
        IMapper mapper,
        IShopGRPCClient shopGRPCClient,
        IRabbitMqClient rabbitMqClient
    ) : ProductBase(productRepository, mapper, shopGRPCClient, rabbitMqClient)
    {
        private readonly IClothingRepository _clothingRepository = clothingRepository;
        public override async Task<Product> CreateProduct(CreateProductCommand request)
        {
            Product product = await Create(request);

            Clothing clothing = JsonSerializer.Deserialize<Clothing>(product.ProductAttributes);
            clothing.ProductId = product.Id;
            await _clothingRepository.CreateAsync(clothing);

            return product;
        }

        public override async Task<BaseResponse> DeleteProduct(DeleteProductCommand request)
        {
            BaseResponse result = await Delete(request);
            if (result.IsSuccess) {
                await _clothingRepository.DeleteByProductId(request.DeleteProductReq.Id);
            }
            else throw new Exception("Internal Server");
            return result;
        }

        public override async Task<BaseResponse> UpdateProduct(UpdateProductCommand request)
        {
            BaseResponse result = await Update(request);
            if (result.IsSuccess) {
                Clothing foundProduct = await _clothingRepository.GetByIdAsync(request.UpdateProductReq.Id);
                if (request.UpdateProductReq.ProductAttributes != null) {
                    Clothing product = JsonSerializer.Deserialize<Clothing>(JsonSerializer.Serialize(request.UpdateProductReq.ProductAttributes));

                    foundProduct.Sizes = product.Sizes ?? foundProduct.Sizes;
                    foundProduct.Color = product.Color ?? foundProduct.Color;
                    foundProduct.Brand = product.Brand ?? foundProduct.Brand;

                    await _clothingRepository.UpdateAsync(foundProduct);
                }
            }
            else throw new Exception("Internal Server");
            return result;
        }
    }
}