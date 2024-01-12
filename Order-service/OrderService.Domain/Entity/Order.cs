using OrderService.Domain.Common;

namespace OrderService.Domain.Entity
{
    public class Order: BaseEntity
    {
        public Guid UserId { get; set; }
        public Guid ShopId {get; set;}
        public string OrderAddress { get; set; }
        public OrderState OrderState { get; set; }
        public List<OrderProduct> OrderProducts { get; set; }
        public OrderCheckout OrderCheckout { get; set; }
    }
}
