
using OrderService.Application.Dto.Order;
using OrderService.Application.Dto.OrderProductDto;

namespace OrderService.Application.Dto.OrderCheckout
{
    public record CheckoutProductReq
    {
        public Guid ShopId {get; set;}
        public List<string> ProductIds { get; set; }
        public OrderDiscount Discount {get; set;}
    }
}