using System.ComponentModel.DataAnnotations;


namespace ProductService.Application.Dto.Product
{
    public record CreateProductReq
    {
        [Required]
        public string ProductName { get; set; }
        [Required]
        public string ProductThumb { get; set; }
        [Required]
        public string ProductDescription { get; set; }
        [Required]
        public double ProductPrice { get; set; }
        [Required]
        public ProductType ProductType { get; set; }
        [Required]
        public Guid ProductShop { get; set; }
        [Required]
        public List<string> ProductImages { get; set; }
        public object ProductAttributes {get; set;}
    }
}