

using MediatR;
using OrderService.Application.Dto;
using OrderService.Domain.Entity;

namespace OrderService.Application.Feature.OrderFeature.Query.GetOrderDetail
{
    public record GetOrderDetailQuery: IRequest<Order>
    {
        public UserDecode User {get; set;}
        public Guid OrderId {get; set;}
    }
}