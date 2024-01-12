

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ProductService.Domain.Common;
using ProductService.Domain.Constant;

namespace ProductService.Domain.Entity
{
    public class Discount: BaseEntity
    {
        [Required]
        public string DiscountName { get; set; } // Name of discount
        [Required]
        public string DiscountDescription { get; set; }
        [Required]
        public DiscountType DiscountType { get; set; }
        [Required]
        public ReducePriceType ReducePriceType { get; set; } 
        [Required]
        public decimal DiscountValue { get; set; } // How much discount
        [Required]
        public string DiscountCode { get; set; } //Code (PAKSDALS)
        [Required]
        public DateTime DiscountStartDate { get; set; }
        [Required]
        public DateTime DiscountEndDate { get; set; }
        [Required]
        public int DiscountMaxUses { get; set; } // Quantity discount applied
        [Required]
        public int DiscountUsesCount { get; set; } = 0; // Quantity of discount is used
        [Column(TypeName = "text[]")]
        public List<string> DiscountUsersUsed { get; set; } // User is used discount
        [Required]
        public int DiscountMaxUsesPerUser { get; set; } // Quantity discount max used for each user
        public decimal? DiscountMinOrderValue { get; set; } = 0;  // Minimun order price to use discount
        public decimal? DiscountMaxOrderValue { get; set; }
        public Guid DiscountShopId { get; set; }
        [Required]
        public bool DiscountIsActive { get; set; } = true;
        [Required]
        public DiscountApply DiscountAppliesTo { get; set; } = DiscountApply.ALL;
        [Column(TypeName = "text[]")]
        public List<Guid> DiscountProductIds { get; set; } = [];
    }
}