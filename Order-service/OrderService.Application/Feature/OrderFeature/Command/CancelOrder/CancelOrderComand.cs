
using MediatR;
using OrderService.Application.Dto;
using OrderService.Application.Response;

namespace OrderService.Application.Feature.OrderFeature.Command.CancelOrder
{
    public record CancelOrderComand: IRequest<BaseResponse>
    {
        public UserDecode User {get; set;}
        public Guid OrderId {get; set;}
    }
}