

using MediatR;
using OrderService.Application.Contract.Infrastructure;
using OrderService.Application.Contract.Persistence;
using OrderService.Application.Exceptions;
using OrderService.Application.Response;
using OrderService.Domain.Entity;
using ShopGRPCService;

namespace OrderService.Application.Feature.OrderFeature.Command.CancelOrder
{
    public class CancelOrderCommandHandler
    (
        IOrderRepository orderRepository,
        IShopGRPCClient shopGRPCClient
    ) : IRequestHandler<CancelOrderComand, BaseResponse>
    {
        private readonly IOrderRepository _orderRepository = orderRepository;
        private readonly IShopGRPCClient _shopGRPCClient = shopGRPCClient;

        public async Task<BaseResponse> Handle(CancelOrderComand request, CancellationToken cancellationToken)
        {
            Order foundOrder = await _orderRepository.GetByIdAsync(request.OrderId)
                ?? throw new NotFoundException("Order not found");

            GetShopRes foundShop = await _shopGRPCClient.GetShopAsync(foundOrder.ShopId.ToString());

            if (
                request.User.UserId != foundOrder.UserId && 
                !foundShop.ShopOwner.Contains(request.User.UserId.ToString())
            )
                throw new ForbiddenException("Not permission!");
            

            foundOrder.OrderState = OrderState.Canceled;

            await _orderRepository.UpdateAsync(foundOrder);

            // Update quantity

            // If shop cancel order, notify for User
            if (foundShop.ShopOwner.Contains(request.User.UserId.ToString()))
            {
                
            }
            return new BaseResponse()
            {
                IsError = false,
                IsSuccess = true,
                Message = "Cancel Order successfully!"
            };
        }
    }
}