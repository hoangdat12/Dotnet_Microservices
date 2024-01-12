
namespace ProductService.Domain.Entity
{
    public class Furniture: ProductBase
    {
        public List<string> Color {get; set;} = [];
        public string Brand {get; set;}
        public string Material {get; set;}
    }
}