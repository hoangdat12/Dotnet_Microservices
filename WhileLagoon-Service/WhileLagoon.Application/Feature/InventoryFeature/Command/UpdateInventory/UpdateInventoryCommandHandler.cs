using MediatR;
using WhileLagoon.Application.Contract.Repository;
using WhileLagoon.Application.Exceptions;
using WhileLagoon.Application.Response;
using WhileLagoon.Domain.Entity;

namespace WhileLagoon.Application.Feature.InventoryFeature.Command.ChangeQuantity
{
    public class UpdateInventoryCommandHandler(
        IInventoryRepository inventoryRepository,
        IShopRepository shopRepository
    ) : IRequestHandler<UpdateInventoryCommand, BaseResponse>
    {
        private readonly IInventoryRepository _inventoryRepository = inventoryRepository;
        private readonly IShopRepository _shopRepository = shopRepository;

        public async Task<BaseResponse> Handle(UpdateInventoryCommand request, CancellationToken cancellationToken)
        {
            Inventory foundInventory = await _inventoryRepository.GetByProductIdAsync(request.Req.ProductId)
                ?? throw new NotFoundException("Product Not found!");

            Shop foundShop = await _shopRepository.GetByIdAsync(foundInventory.ShopId);

            if (!foundShop.ShopOwner.Contains(request.User.Id.ToString())) 
                throw new ForbiddenException("Not permission!");

            foundInventory.Quantity = request.Req.Quanttiy.Equals(null) 
                ? request.Req.Quanttiy : foundInventory.Quantity;
            foundInventory.Location = request.Req.Location.Equals(null) 
                ? request.Req.Location : foundInventory.Location;

            if (foundInventory.Quantity < 0) foundInventory.Quantity = 0;

            await _inventoryRepository.UpdateAsync(foundInventory);

            return new BaseResponse() 
            {
                IsSuccess = true,
                IsError = false
            };
        }
    }
}