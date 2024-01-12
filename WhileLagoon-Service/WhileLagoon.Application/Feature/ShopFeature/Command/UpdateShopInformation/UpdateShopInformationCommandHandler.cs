using MediatR;
using WhileLagoon.Application.Contract.Repository;
using WhileLagoon.Application.Exceptions;
using WhileLagoon.Domain.Entity;

namespace WhileLagoon.Application.Feature.ShopFeature.Command.UpdateShopInformation
{
    public class UpdateShopInformationCommandHandler(
        IShopRepository shopRepository
    ) : IRequestHandler<UpdateShopInformationCommand, Shop>
    {
        private readonly IShopRepository _shopRepository = shopRepository;
        public async Task<Shop> Handle(UpdateShopInformationCommand request, CancellationToken cancellationToken)
        {
            Shop foundShop = await _shopRepository.GetByIdAsync(request.UpdateShopInfor.ShopId)
                ?? throw new NotFoundException("Shop not found!");

            if (
                foundShop.ShopOwner.Contains(request.User.Id.ToString())
                && request.User.Role == Domain.Enum.Role.USER
            )
                throw new BadRequestException("Not permission to perform this action!");

            foundShop.ShopName ??= request.UpdateShopInfor?.ShopName;
            foundShop.ShopDescription ??= request.UpdateShopInfor?.ShopDescription;
            foundShop.ShopCategory = [.. foundShop.ShopCategory, .. request.UpdateShopInfor.ShopCategory];

            await _shopRepository.UpdateAsync(foundShop);
            return foundShop;
        }
    }
}
