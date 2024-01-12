

using System.ComponentModel.DataAnnotations;
using ProductService.Domain.Constant;

namespace ProductService.Application.Dto.Discount
{
    public record CreateDiscountReq
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
        public int DiscountMaxUsesPerUser { get; set; } // Quantity discount max used for each user
        public decimal? DiscountMinOrderValue { get; set; } = 0;  // Minimun order price to use discount
        public decimal? DiscountMaxOrderValue { get; set; }
        public Guid DiscountShopId { get; set; }
        [Required]
        public DiscountApply DiscountAppliesTo { get; set; } = DiscountApply.ALL;
        public List<Guid> DiscountProductIds { get; set; } = [];
    }
}