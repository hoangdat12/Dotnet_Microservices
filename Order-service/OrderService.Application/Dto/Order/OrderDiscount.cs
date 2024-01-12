

namespace OrderService.Application.Dto.Order
{
    public record OrderDiscount
    {
        public string ShopDiscount { get; set; }
        public string ShipDiscount {get; set;}
    }
}