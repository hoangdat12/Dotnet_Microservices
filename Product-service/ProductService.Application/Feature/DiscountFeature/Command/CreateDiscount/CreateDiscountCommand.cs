using MediatR;
using ProductService.Application.Dto;
using ProductService.Application.Dto.Discount;
using ProductService.Domain.Entity;

namespace ProductService.Application.Feature.DiscountFeature.Command.CreateDiscount
{
    public record CreateDiscountCommand: IRequest<Discount>
    {
        public UserDecode User {get; set;}
        public CreateDiscountReq CreateDiscountReq {get; set;}
    }
}