namespace OrderService.Application.Dto.OrderCheckout
{
    public record OrderCheckoutReq
    {
        public string ShopDiscount { get; set; }
        public string Discount { get; set; }
        public string ShipDiscount {get; set;}
        public double OrderTotalPrice { get; set; }
        public double OrderDiscount { get; set; }
        public double OrderShipPrice {get; set;}
        public double OrderActualPrice { get; set; }

    }
}
