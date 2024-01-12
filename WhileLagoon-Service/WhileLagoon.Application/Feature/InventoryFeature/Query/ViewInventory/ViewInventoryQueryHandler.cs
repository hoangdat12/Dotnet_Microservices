using MediatR;
using WhileLagoon.Application.Contract.Repository;
using WhileLagoon.Application.Exceptions;
using WhileLagoon.Domain.Entity;

namespace WhileLagoon.Application.Feature.InventoryFeature.Query.ViewInventory
{
    public class ViewInventoryQueryHandler
    (
        IInventoryRepository inventoryRepository,
        IShopRepository shopRepository
    ) : IRequestHandler<ViewInventoryQuery, Inventory>
    {
        private readonly IInventoryRepository _inventoryRepository = inventoryRepository;
        private readonly IShopRepository _shopRepository = shopRepository;

        public async Task<Inventory> Handle(ViewInventoryQuery request, CancellationToken cancellationToken)
        {
            Inventory foundInventory = await _inventoryRepository.GetByProductIdAsync(request.ProductId)
                ?? throw new NotFoundException("Product not found!");

            Shop foundShop = await _shopRepository.GetByIdAsync(foundInventory.ShopId)
                ?? throw new NotFoundException("Shop not found!");

            if (!foundShop.ShopOwner.Contains(request.User.Id.ToString())) 
                throw new ForbiddenException("Not permission!");

            return foundInventory;
        }
    }
}