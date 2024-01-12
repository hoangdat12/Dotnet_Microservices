
using System.ComponentModel.DataAnnotations;

namespace ProductService.Application.Dto.Product
{
    public record DeleteProductReq
    {
        [Required]
        public Guid Id {get; set;}
        [Required]
        public ProductType ProductType {get; set;}
    }
}