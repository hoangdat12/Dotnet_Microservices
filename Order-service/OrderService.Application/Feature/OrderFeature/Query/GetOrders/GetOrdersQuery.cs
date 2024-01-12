

using MediatR;
using OrderService.Application.Dto;
using OrderService.Domain.Entity;

namespace OrderService.Application.Feature.OrderFeature.Query.GetOrders
{
    public record GetOrdersQuery: IRequest<List<Order>>
    {
        public UserDecode User {get; set;}
        public Pagination Pagination {get; set;}
    }
}