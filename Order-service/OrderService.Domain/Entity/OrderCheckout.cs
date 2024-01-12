using System.Text.Json.Serialization;
using OrderService.Domain.Common;

namespace OrderService.Domain.Entity
{
    public class OrderCheckout: BaseEntity
    {
        [JsonIgnore]
        public Order Order { get; set; }
        public Guid OrderId { get; set; }
        public string ShopDiscount { get; set; }
        public string Discount { get; set; }
        public string ShipDiscount {get; set;}
        public double OrderTotalPrice { get; set; }
        public double OrderDiscount { get; set; }
        public double OrderShipPrice {get; set;}
        public double OrderActualPrice { get; set; }
    }
}
