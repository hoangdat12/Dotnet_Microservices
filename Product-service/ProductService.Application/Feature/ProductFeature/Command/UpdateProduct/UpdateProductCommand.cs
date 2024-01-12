using MediatR;
using ProductService.Application.Dto;
using ProductService.Application.Dto.Product;
using ProductService.Application.Response;


namespace ProductService.Application.Feature.ProductFeature.Command.UpdateProduct
{
    public record UpdateProductCommand: IRequest<BaseResponse>
    {
        public UserDecode User {get; set;}
        public UpdateProductReq UpdateProductReq {get; set;}
    }
}