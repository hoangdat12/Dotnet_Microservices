
using MediatR;
using ProductService.Application.Dto;
using ProductService.Application.Response;

namespace ProductService.Application.Feature.DiscountFeature.Command.DisableDiscount
{
    public record DisableDiscountCommand: IRequest<BaseResponse>
    {
        public UserDecode User {get; set;}
        public Guid DiscountId {get; set;}
    }
}