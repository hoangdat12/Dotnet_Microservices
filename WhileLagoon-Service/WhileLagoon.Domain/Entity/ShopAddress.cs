using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WhileLagoon.Domain.Common;
using System.Text.Json.Serialization;

namespace WhileLagoon.Domain.Entity
{
    public class ShopAddress: BaseEntity
    {
        [Required]
        public string AddressUserName { get; set; }

        [Required]
        public string AddressPhone { get; set; }

        [Required]
        public string AddressCountry {  get; set; }

        [Required]
        public string AddressCity { get; set; }
        [Required]
        public string AddresState { get; set; }

        [Required]
        public string AddressDetail { get; set; }
        [JsonIgnore]
        public Guid ShopId { get; set; }

        [ForeignKey("ShopId")]
        [JsonIgnore]
        public Shop Shop { get; set; }
    }
}
