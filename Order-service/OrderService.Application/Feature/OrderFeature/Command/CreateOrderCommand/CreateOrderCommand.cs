using MediatR;
using OrderService.Application.Dto;
using OrderService.Application.Dto.Order;
using OrderService.Domain.Entity;

namespace OrderService.Application.Feature.OrderFeature.Command.CreateOrderCommand
{
    public record CreateOrderCommand: IRequest<List<Order>>
    {
        public UserDecode User { get; set; }
        public List<CreateOrderReq> CreateOrdersReq { get; set; }
    }
}
