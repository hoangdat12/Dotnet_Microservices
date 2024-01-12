using ProductService.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Text.Json.Serialization;


namespace ProductService.Domain.Entity
{
    public class Product: BaseEntity
    {

        [Required]
        [MaxLength(255)]
        public string ProductName { get; set; }

        [Required]
        public string ProductThumb { get; set; }

        [JsonIgnore]
        public string ProductSlug { get; set; }

        [Column(TypeName = "text")]
        public string ProductDescription { get; set; }

        [Required]
        public double ProductPrice { get; set; }

        [Required]
        [Column(TypeName = "text")]
        public ProductType ProductType { get; set; }

        [Required]
        public Guid ProductShop { get; set; } 

        [Required]
        [DefaultValue(4.5)]
        public double ProductRatingAverage { get; set; }
        
        [Required]
        [Column(TypeName = "jsonb")]
        public string ProductAttributes { get; set; } 

        [DefaultValue(true)]
        [JsonIgnore]
        public bool IsDraft { get; set; }

        [DefaultValue(false)]
        [JsonIgnore]
        public bool IsPublished { get; set; }

        [Column(TypeName = "text[]")]
        public List<string> ProductImages { get; set;}
    }
}
