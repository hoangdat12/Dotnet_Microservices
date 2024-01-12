
using System.ComponentModel.DataAnnotations;

namespace ProductService.Domain.Entity
{
    public class ProductBase
    {
        [Key]
        public Guid ProductId {get; set;}
    }
}