
using MediatR;
using WhileLagoon.Domain.Entity;

namespace WhileLagoon.Application.Feature.InventoryFeature.Query.ViewInventory
{
    public record ViewInventoryQuery: IRequest<Inventory>
    {
        public User User {get; set;}
        public Guid ProductId {get; set;}
    }
}