
namespace ProductService.Domain.Entity
{
    public class Clothing: ProductBase
    {
        public List<string> Sizes {get; set;} = [];
        public List<string> Color {get; set;} = [];
        public string Brand {get; set;}
    }
}