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
    public class ElectronicService
    (
        IProductRepository productRepository,
        IMapper mapper,
        IElectronicRepository electronicRepository,
        IShopGRPCClient shopGRPCClient,
        IRabbitMqClient rabbitMqClient
    ) : ProductBase(productRepository, mapper, shopGRPCClient, rabbitMqClient)
    {
        private readonly IElectronicRepository _electronicRepository = electronicRepository;
        public override async Task<Product> CreateProduct(CreateProductCommand request)
        {
            Product product = await Create(request);

            Electronic electronic = JsonSerializer.Deserialize<Electronic>(product.ProductAttributes);
            electronic.ProductId = product.Id;
            await _electronicRepository.CreateAsync(electronic);

            return product;
        }
        
        public override async Task<BaseResponse> DeleteProduct(DeleteProductCommand request)
        {
            BaseResponse result = await Delete(request);
            if (result.IsSuccess) {
                await _electronicRepository.DeleteByProductId(request.DeleteProductReq.Id);
            }
            else throw new Exception("Internal Server");
            return result;
        }

        public override async Task<BaseResponse> UpdateProduct(UpdateProductCommand request)
        {
            BaseResponse result = await Update(request);
            if (result.IsSuccess) {
                Electronic foundProduct = await _electronicRepository.GetByIdAsync(request.UpdateProductReq.Id);
                if (request.UpdateProductReq.ProductAttributes != null) {
                    Electronic product = JsonSerializer.Deserialize<Electronic>(JsonSerializer.Serialize(request.UpdateProductReq.ProductAttributes));

                    foundProduct.Color = product.Color ?? foundProduct.Color;
                    foundProduct.Material = product.Material ?? foundProduct.Material;
                    foundProduct.Brand = product.Brand ?? foundProduct.Brand;

                    await _electronicRepository.UpdateAsync(foundProduct);
                }
            }
            else throw new Exception("Internal Server");
            return result;
        }
    }
}