using System.ComponentModel.DataAnnotations;
using MediatR;
using ProductService.Application.Dto;
using ProductService.Application.Dto.Product;
using ProductService.Domain.Entity;


namespace ProductService.Application.Feature.ProductFeature.Command.CreateProduct
{
    public record CreateProductCommand: IRequest<Product>
    {
        public UserDecode User {get; set;}
        public CreateProductReq CreateProductReq {get; set;}
    }
}
