using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using WhileLagoon.Domain.Common;

namespace WhileLagoon.Domain.Entity
{
    public class Shop: BaseEntity
    {
        [Column(TypeName = "text[]")]
        [Required]
        [JsonIgnore]
        public List<string> ShopOwner { get; set; } = new List<string>();

        [StringLength(150)]
        [Required]
        public string ShopName { get; set; }

        [Column(TypeName = "text")]
        public string ShopDescription { get; set; }

        public int ShopTotalProduct { get; set; } = 0;
        [Required]
        public string ShopAvatar { get; set; }

        public int ShopFollowers { get; set; } = 0;

        public int ShopFollowing { get; set; } = 0;

        [Column(TypeName = "text[]")]
        [Required]
        public List<string> ShopCategory { get; set; } = new List<string>();

        public int ShopEvaluate { get; set; } = 0;

        // Navigation property for the one-to-many relationship
        [JsonIgnore]
        public List<ShopAddress> Addresses { get; set; }
    }
}
