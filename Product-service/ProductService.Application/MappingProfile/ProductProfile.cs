using AutoMapper;
using ProductService.Application.Dto;
using ProductService.Application.Dto.Product;
using ProductService.Domain.Entity;


namespace ProductService.Application.MappingProfile
{
    public class ProductProfile: Profile
    {
       public ProductProfile() {
            CreateMap<ProductDto, Product>().ReverseMap();
            CreateMap<CreateProductReq, Product>();
            CreateMap<UpdateProductReq, Product>().ReverseMap();
            CreateMap<Product, GetProductRes>();
       }
    }
}
