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
    public class FurnitureService
    (
        IProductRepository productRepository,
        IMapper mapper,
        IFurnitureRepository furnitureRepository,
        IShopGRPCClient shopGRPCClient,
        IRabbitMqClient rabbitMqClient
    ): ProductBase(productRepository, mapper, shopGRPCClient, rabbitMqClient)
    {
        private readonly IFurnitureRepository _furnitureRepository = furnitureRepository;

        public override async Task<Product> CreateProduct(CreateProductCommand request)
        {
            Product product = await Create(request);

            Furniture furniture = JsonSerializer.Deserialize<Furniture>(product.ProductAttributes);
            furniture.ProductId = product.Id;
            await _furnitureRepository.CreateAsync(furniture);

            return product;
        } 
        
        public override async Task<BaseResponse> DeleteProduct(DeleteProductCommand request)
        {
            BaseResponse result = await Delete(request);
            if (result.IsSuccess) {
                await _furnitureRepository.DeleteByProductId(request.DeleteProductReq.Id);
            }
            else throw new Exception("Internal Server");
            return result;
        }

        public override async Task<BaseResponse> UpdateProduct(UpdateProductCommand request)
        {
            BaseResponse result = await Update(request);
            if (result.IsSuccess) {
                Furniture foundProduct = await _furnitureRepository.GetByIdAsync(request.UpdateProductReq.Id);
                if (request.UpdateProductReq.ProductAttributes != null) {
                    Furniture product = JsonSerializer.Deserialize<Furniture>(JsonSerializer.Serialize(request.UpdateProductReq.ProductAttributes));

                    foundProduct.Color = product.Color ?? foundProduct.Color;
                    foundProduct.Material = product.Material ?? foundProduct.Material;
                    foundProduct.Brand = product.Brand ?? foundProduct.Brand;

                    await _furnitureRepository.UpdateAsync(foundProduct);
                }
            }
            else throw new Exception("Internal Server");
            return result;
        }
    }
}