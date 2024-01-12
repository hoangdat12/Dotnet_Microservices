using MediatR;
using ProductService.Application.Dto;
using ProductService.Application.Dto.Product;
using ProductService.Application.Response;

namespace ProductService.Application.Feature.ProductFeature.Command.DeleteProduct
{
    public record DeleteProductCommand: IRequest<BaseResponse>
    {
        public UserDecode User {get; set;}
        public DeleteProductReq DeleteProductReq {get; set;}
    }
}