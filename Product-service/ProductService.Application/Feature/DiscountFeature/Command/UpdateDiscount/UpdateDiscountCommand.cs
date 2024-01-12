

using MediatR;
using ProductService.Application.Dto;
using ProductService.Application.Dto.Discount;
using ProductService.Domain.Entity;

namespace ProductService.Application.Feature.DiscountFeature.Command.UpdateDiscount
{
    public record UpdateDiscountCommand: IRequest<Discount>
    {
        public UserDecode User {get; set;}
        public UpdateDiscountReq UpdateDiscountReq {get; set;}
    }
}