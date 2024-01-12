
namespace WhileLagoon.Application.Dto.Inventory
{
    public record UpdateInventoryReq
    {
        public Guid ProductId {get; set;}
        public int Quanttiy {get; set;}
        public string Location {get; set;}
    }
}