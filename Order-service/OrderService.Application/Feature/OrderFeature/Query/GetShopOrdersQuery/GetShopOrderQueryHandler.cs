

using MediatR;
using OrderService.Application.Contract.Infrastructure;
using OrderService.Application.Contract.Persistence;
using OrderService.Domain.Entity;

namespace OrderService.Application.Feature.OrderFeature.Query.GetShopOrdersQuery
{


    public class GetShopOrderQueryHandler(
        IOrderRepository orderRepository,
        IShopGRPCClient shopGRPCClient
    ) : IRequestHandler<GetShopOrderQuery, List<Order>>
    {
        private readonly IOrderRepository _orderRepository = orderRepository;
        private readonly IShopGRPCClient _shopGRPCClient = shopGRPCClient;

        public async Task<List<Order>> Handle(GetShopOrderQuery request, CancellationToken cancellationToken)
        {
           return await _orderRepository.GetByShopIdAsync(request.User.UserId, request.Pagination);
        }
    }
}