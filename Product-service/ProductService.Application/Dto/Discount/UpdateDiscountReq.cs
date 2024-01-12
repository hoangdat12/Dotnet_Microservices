
using System.ComponentModel.DataAnnotations;
using ProductService.Domain.Constant;

namespace ProductService.Application.Dto.Discount
{
    public class UpdateDiscountReq
    {
        [Required]
        public Guid DiscountId {get; set;}
        public string DiscountName { get; set; } // Name of discount
        public string DiscountDescription { get; set; }
        public DiscountType DiscountType { get; set; }
        public ReducePriceType ReducePriceType { get; set; } 
        public decimal DiscountValue { get; set; } // How much discount
        public DateTime DiscountStartDate { get; set; }
        public DateTime DiscountEndDate { get; set; }
        public int DiscountMaxUses { get; set; } // Quantity discount applied
        public int DiscountMaxUsesPerUser { get; set; } // Quantity discount max used for each user
        public decimal? DiscountMinOrderValue { get; set; } = 0;  // Minimun order price to use discount
        public decimal? DiscountMaxOrderValue { get; set; }
        public DiscountApply DiscountAppliesTo { get; set; } = DiscountApply.ALL;
        public List<Guid> DiscountProductIds { get; set; } = [];
    }
}