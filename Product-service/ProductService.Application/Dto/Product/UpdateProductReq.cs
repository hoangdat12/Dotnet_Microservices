

using System.ComponentModel.DataAnnotations;

namespace ProductService.Application.Dto.Product
{
    public record UpdateProductReq
    {
        [Required]
        public Guid Id {get; set;}
        [Required]
        public ProductType ProductType {get; set;}
        public string ProductName { get; set; }

        public string ProductThumb { get; set; }

        public string ProductDescription { get; set; }

        public double? ProductPrice { get; set; }

        public List<string> ProductImages { get; set; }

         public object ProductAttributes {get; set;}
    }
}