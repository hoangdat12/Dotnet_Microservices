
namespace ProductService.Domain.Entity
{
    public class Electronic: ProductBase
    {
        public List<string> Color {get; set;} = [];
        public string Brand {get; set;}
        public string Material {get; set;}
    }
}