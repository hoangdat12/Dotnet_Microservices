using OrderService.Application.Dto.OrderCheckout;

namespace OrderService.Application.Dto.Order
{
    public record CreateOrderReq
    {
        public List<Guid> Products { get; set; }
        public Guid ShopId {get; set;}
        public OrderCheckoutReq OrderCheckout { get; set; }
        public string OrderAddress {get; set;}
    }
}
