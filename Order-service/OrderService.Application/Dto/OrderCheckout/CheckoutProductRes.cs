
using ProductService;
using ShopGRPCService;

namespace OrderService.Application.Dto.OrderCheckout
{
    public record CheckoutProductRes
    {
        public OrderCheckoutReq Checkout {get; set;}
        public Products Products {get; set;}
        public GetShopRes Shop {get; set;}
    }
}