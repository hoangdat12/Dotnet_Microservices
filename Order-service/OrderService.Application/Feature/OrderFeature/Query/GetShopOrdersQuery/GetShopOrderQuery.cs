

using MediatR;
using OrderService.Application.Dto;
using OrderService.Domain.Entity;

namespace OrderService.Application.Feature.OrderFeature.Query.GetShopOrdersQuery
{
    public record GetShopOrderQuery: IRequest<List<Order>>
    {
        public UserDecode User {get; set;}
        public Guid ShopId {get; set;}
        public Pagination Pagination {get; set;}
    }
}