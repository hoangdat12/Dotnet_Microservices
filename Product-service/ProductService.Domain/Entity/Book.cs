
namespace ProductService.Domain.Entity
{
    public class Book: ProductBase
    {
        public string Language {get; set;}
        public string PublicYear {get; set;}
        public string Author {get; set;}
    }
}