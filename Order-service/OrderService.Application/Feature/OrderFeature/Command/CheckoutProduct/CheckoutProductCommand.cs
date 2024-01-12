using MediatR;
using OrderService.Application.Dto.OrderCheckout;

namespace OrderService.Application.Feature.OrderFeature.Command.CheckoutProduct
{
    public record CheckoutProductCommand: IRequest<List<CheckoutProductRes>>
    {
        public CheckoutProductReq[] CheckoutProductsReq {get; set;}
        public string Discount {get; set;}
    }
}