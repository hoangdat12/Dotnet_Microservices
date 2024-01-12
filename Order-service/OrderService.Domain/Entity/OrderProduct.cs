using System.Text.Json.Serialization;
using OrderService.Domain.Common;

namespace OrderService.Domain.Entity
{
    public class OrderProduct: BaseEntity
    {
        public Guid ProductId { get; set; }
        public Guid OrderId { get; set; }
        [JsonIgnore]
        public Order Order { get; set; }
        public string ProductName { get; set; }
        public string ProductThumb { get; set; }
        public double ProductPrice { get; set; }
    }
}
