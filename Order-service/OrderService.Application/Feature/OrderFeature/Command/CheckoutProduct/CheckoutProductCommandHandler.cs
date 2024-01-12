
using MediatR;
using OrderService.Application.Contract.Infrastructure;
using OrderService.Application.Contract.Infrastructure.gRPC;
using OrderService.Application.Dto.OrderCheckout;
using OrderService.Application.Exceptions;
using ProductService;
using ShopGRPCService;

namespace OrderService.Application.Feature.OrderFeature.Command.CheckoutProduct
{
    public class CheckoutProductCommandHandler
    (
        IProductGrpcClient productGrpcClient,
        IShopGRPCClient shopGRPCClient
    ) : IRequestHandler<CheckoutProductCommand, List<CheckoutProductRes>>
    {
        private readonly IProductGrpcClient _productGrpcClient = productGrpcClient;
        private readonly IShopGRPCClient _shopGRPCClient = shopGRPCClient;

        public async Task<List<CheckoutProductRes>> Handle(CheckoutProductCommand request, CancellationToken cancellationToken)
        {
            double OrderDiscount = 0 / request.CheckoutProductsReq.Length;

            List<CheckoutProductRes> response = [];

            // Check discount
            foreach (var CheckoutProductReq in request.CheckoutProductsReq)
            {
                string ShopId = CheckoutProductReq.ShopId.ToString();

                GetPriceRes res = await _productGrpcClient.GetPriceAsync(
                    CheckoutProductReq.ProductIds,
                    ShopId
                );

                GetShopRes foundShop = await _shopGRPCClient.GetShopAsync(ShopId);
                if (foundShop.Equals(null))
                    throw new NotFoundException("Shop not found!");

                if (CheckoutProductReq.ProductIds.Count != res.Products.Products_.Count)
                    throw new BadRequestException("Some product not found!");

                double ShopDiscount = 0;
                double FeeShipDiscount = 0;

                OrderCheckoutReq orderCheckoutReq = new()
                {
                    ShopDiscount = CheckoutProductReq.Discount.ShopDiscount,
                    Discount = request.Discount,
                    ShipDiscount = CheckoutProductReq.Discount.ShipDiscount,
                    OrderTotalPrice = res.Price,
                    OrderDiscount = OrderDiscount + ShopDiscount,
                    OrderShipPrice = FeeShipDiscount,
                    OrderActualPrice = res.Price - OrderDiscount - ShopDiscount -FeeShipDiscount
                };

                CheckoutProductRes checkoutProductRes = new()
                {
                    Checkout = orderCheckoutReq,
                    Products = res.Products,
                    Shop = foundShop
                };

                response.Add(checkoutProductRes);
            }

            return response;
        }
    }
}