using System.ComponentModel.DataAnnotations;
using MediatR;
using WhileLagoon.Application.Dto.Cart;
using WhileLagoon.Domain.Entity;

namespace WhileLagoon.Application.Feature.CartFeature.Command.AddProduct
{
    public record AddProductCommand: IRequest<CartProduct>
    {
        public AddProductToCart Product {get; set;}
        public User User {get; set;}
    }
}