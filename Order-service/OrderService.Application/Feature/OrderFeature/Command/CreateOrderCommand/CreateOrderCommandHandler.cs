using MediatR;
using OrderService.Application.Contract.Infrastructure.gRPC;
using OrderService.Application.Contract.Persistence;
using OrderService.Application.Exceptions;
using OrderService.Domain.Entity;
using ProductService;

namespace OrderService.Application.Feature.OrderFeature.Command.CreateOrderCommand
{
    public class CreateOrderCommandHandler(
        IProductGrpcClient productGrpcClient,
        IOrderRepository orderRepository
    ) 
        : IRequestHandler<CreateOrderCommand, List<Order>>
    {
        private readonly IProductGrpcClient _productGrpcClient = productGrpcClient;
        private readonly IOrderRepository _orderRepository = orderRepository;

        public async Task<List<Order>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            // Check discount
            List<Order> response = [];
            foreach (var CreateOrderReq in request.CreateOrdersReq)
            {
                var orderCheckout = CreateOrderReq.OrderCheckout;

                List<string> productIds = CreateOrderReq.Products.Select(x => x.ToString()).ToList();

                GetPriceRes res = await _productGrpcClient.GetPriceAsync(
                    productIds, 
                    CreateOrderReq.ShopId.ToString()
                );
                if (res.Price != orderCheckout.OrderTotalPrice)
                    throw new BadRequestException("Some Discounts have expired, please try again!");
                if (CreateOrderReq.Products.Count != res.Products.Products_.Count) 
                    throw new BadRequestException("Some product not exist, please try again!");

                Order order = new()
                {
                    UserId = request.User.UserId,
                    ShopId = CreateOrderReq.ShopId,
                    OrderCheckout = new OrderCheckout
                    {
                        OrderTotalPrice = orderCheckout.OrderTotalPrice,
                        OrderDiscount = orderCheckout.OrderDiscount,
                        OrderShipPrice = orderCheckout.OrderShipPrice,
                        OrderActualPrice = orderCheckout.OrderActualPrice,
                        ShopDiscount = orderCheckout.ShopDiscount,
                        Discount = orderCheckout.Discount,
                        ShipDiscount = orderCheckout.ShipDiscount
                    },
                    OrderProducts = res.Products.Products_.Select(productReq => new OrderProduct
                    {
                        ProductId = Guid.Parse(productReq.Id),
                        ProductName = productReq.ProductName,
                        ProductThumb = productReq.ProductThumb,
                        ProductPrice = productReq.ProductPrice,
                    }).ToList(),
                    OrderAddress = CreateOrderReq.OrderAddress,
                    OrderState = OrderState.Pending
                };

                order = await _orderRepository.CreateAsync(order);

                response.Add(order);
            }

            // Minus Inventory

            // Notify  

            // _orderRepository.EndTransaction();

            return response;
        }
    }
}
