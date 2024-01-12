
using MediatR;
using WhileLagoon.Application.Dto.Cart;
using WhileLagoon.Application.Response;
using WhileLagoon.Domain.Entity;

namespace WhileLagoon.Application.Feature.CartFeature.Command.ChangeQuantity
{
    public class ChangeQuantityCommand: IRequest<BaseResponse>
    {
        public AddProductToCart Product {get; set;}
        public User User {get; set;}
    }
}