namespace WhileLagoon.Application.Dto.Shop
{
    public record UpdateShopInfor
    {
        public Guid ShopId { get; set; }
        public string ShopName { get; set; }
        public string ShopDescription { get; set; }
        public string[] ShopCategory { get; set; }
    }
}
