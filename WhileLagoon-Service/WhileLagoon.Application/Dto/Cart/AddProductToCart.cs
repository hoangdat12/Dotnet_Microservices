
using System.ComponentModel.DataAnnotations;

namespace WhileLagoon.Application.Dto.Cart
{
    public record AddProductToCart
    {
        [Required]
        public Guid ProductId {get; set;}
        public int ProductQuantity {get; set;} = 1;
    }
}