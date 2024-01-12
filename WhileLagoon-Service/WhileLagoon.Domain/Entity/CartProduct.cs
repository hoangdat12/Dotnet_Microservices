
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WhileLagoon.Domain.Entity
{
    public class CartProduct
    {
        [Key]
        public Guid ProductId {get; set;}
        [Required]
        public int ProductQuantity {get; set;} = 1;
        [JsonIgnore]
        public Guid CartId {get; set; }
        [JsonIgnore]
        public Cart Cart {get; set;}
        [JsonIgnore]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [JsonIgnore]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}