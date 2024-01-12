using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WhileLagoon.Domain.Entity
{
    public class Inventory
    {
        [Key]
        public Guid ProductId {get; set;}
        public Guid ShopId {get; set;}
        public int Quantity {get; set;}
        public string Location {get; set;}
        [JsonIgnore]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [JsonIgnore]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}