using System.ComponentModel.DataAnnotations;

namespace WhileLagoon.Application.Dto.Shop
{
    public record CreateShopReq
    {
        [Required]
        public string ShopName { get; set; }
        [Required]
        public string ShopDescription { get; set; }
        [Required]
        public string[] ShopCategory { get; set; }
        [Required]
        public string ShopAvatar { get; set; }
        [Required]
        public Guid AddressId { get; set; }
    }
}
