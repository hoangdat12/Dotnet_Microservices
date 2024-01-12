namespace OrderService.Domain.Entity
{
    public enum OrderState
    {
        Pending = 1,
        Processing,
        Shipped,
        Delivered,
        Canceled,
        Refused
    }
}
