

using MediatR;
using OrderService.Application.Contract.Infrastructure;
using OrderService.Application.Contract.Persistence;
using OrderService.Application.Exceptions;
using OrderService.Domain.Entity;
using ShopGRPCService;

namespace OrderService.Application.Feature.OrderFeature.Query.GetOrderDetail
{
    public class GetOrderDetailQueryHandler
    (
        IOrderRepository orderRepository,
        IShopGRPCClient shopGRPCClient
    ) : IRequestHandler<GetOrderDetailQuery, Order>
    {
        private readonly IOrderRepository _orderRepository = orderRepository;
        private readonly IShopGRPCClient _shopGRPCClient = shopGRPCClient;

        public async Task<Order> Handle(GetOrderDetailQuery request, CancellationToken cancellationToken)
        {
            Order foundOrder = await _orderRepository.GetByIdAsync(request.OrderId)
                ?? throw new NotFoundException("Order not found");

            GetShopRes foundShop = await _shopGRPCClient.GetShopAsync(foundOrder.ShopId.ToString());

            if (
                request.User.UserId != foundOrder.UserId && 
                !foundShop.ShopOwner.Contains(request.User.UserId.ToString())
            )
                throw new ForbiddenException("Not permission!");

            return foundOrder;
        }
    }
}