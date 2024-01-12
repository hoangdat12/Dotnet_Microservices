using System.Text.Json;
using AutoMapper;
using ProductService.Application.Constant;
using ProductService.Application.Contract.Infrastructure;
using ProductService.Application.Contract.Persistant;
using ProductService.Application.Contract.RabbitMq;
using ProductService.Application.Event;
using ProductService.Application.Exceptions;
using ProductService.Application.Feature.ProductFeature.Command.CreateProduct;
using ProductService.Application.Feature.ProductFeature.Command.DeleteProduct;
using ProductService.Application.Feature.ProductFeature.Command.UpdateProduct;
using ProductService.Application.Response;
using ProductService.Domain.Entity;
using ProductService.Infrustructure.Service.RabbitMq.Event;
using ShopGRPCService;

namespace ProductService.Infrustructure.Service
{
    public abstract class ProductBase(
        IProductRepository productRepository, 
        IMapper mapper,
        IShopGRPCClient shopGRPCClient,
        IRabbitMqClient rabbitMqClient
    )
    {
        private readonly IProductRepository _productRepository = productRepository;
        private readonly IMapper _mapper = mapper;
        private readonly IShopGRPCClient _shopGRPCClient = shopGRPCClient;
        private readonly IRabbitMqClient _rabbitMqClient = rabbitMqClient;

        public abstract Task<Product> CreateProduct(CreateProductCommand request);
        public abstract Task<BaseResponse> DeleteProduct(DeleteProductCommand request);
        public abstract Task<BaseResponse> UpdateProduct(UpdateProductCommand request);

        protected async Task<Product> Create(CreateProductCommand request)
        {
            Product foundProduct = await _productRepository.IsExistInShop(
                request.CreateProductReq.ProductShop, 
                request.CreateProductReq.ProductName
            );
          
            if (foundProduct is not null)
                throw new BadRequestException("Product is already existed in shop!");

            await IsPermission(
                request.CreateProductReq.ProductShop.ToString(),
                request.User.UserId.ToString()
            );

            var productCreate = _mapper.Map<Product>(request.CreateProductReq);
            productCreate.ProductAttributes = JsonSerializer.Serialize(request.CreateProductReq.ProductAttributes);

            await _productRepository.CreateAsync(productCreate);
            return productCreate;
        }

        protected async Task<BaseResponse> Delete(DeleteProductCommand request)
        {
            Product foundProduct = await _productRepository.GetByIdAsync(request.DeleteProductReq.Id) 
                ?? throw new NotFoundException("Product not found!");

            await IsPermission(
                foundProduct.ProductShop.ToString(),
                request.User.UserId.ToString()
            );

            await _productRepository.DeleteAsync(foundProduct);

            BaseResponse response = new() 
            {
                IsSuccess = true,
                IsError = false
            };

            return response;
        }

        protected async Task<BaseResponse> Update(UpdateProductCommand request) 
        {
            Product foundProduct = await _productRepository.GetByIdAsync(request.UpdateProductReq.Id) 
                ?? throw new NotFoundException("Product not found!");

            await IsPermission(
                foundProduct.ProductShop.ToString(),
                request.User.UserId.ToString()
            );

            foundProduct.ProductName = request.UpdateProductReq.ProductName ?? foundProduct.ProductName;
            foundProduct.ProductThumb = request.UpdateProductReq.ProductThumb ?? foundProduct.ProductThumb;
            foundProduct.ProductDescription = request.UpdateProductReq.ProductDescription ?? foundProduct.ProductDescription;
            foundProduct.ProductPrice = request.UpdateProductReq.ProductPrice ?? foundProduct.ProductPrice;
            foundProduct.ProductImages = request.UpdateProductReq.ProductImages ?? foundProduct.ProductImages;
            await _productRepository.UpdateAsync(foundProduct);

            BaseResponse response = new()
            {
                IsSuccess = true,
                IsError = false
            };

            return response;
        }

        private async Task IsPermission(string ProductShop, string UserId) {
            GetShopRes foundShop = await _shopGRPCClient.GetShopAsync(ProductShop)
                ?? throw new NotFoundException("Shop not found!");

            if (!foundShop.ShopOwner.Contains(UserId)) 
                throw new ForbiddenException("Not permission!");
        }
    }
}