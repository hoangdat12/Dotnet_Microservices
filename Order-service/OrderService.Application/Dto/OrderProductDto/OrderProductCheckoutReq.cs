

namespace OrderService.Application.Dto.OrderProductDto
{
    public record OrderProductCheckoutReq
    {
        public string ProductId { get; set; }
        public Guid ShopId {get; set;}
    }
}