using MediatR;
using WhileLagoon.Application.Contract.Repository;
using WhileLagoon.Application.Exceptions;
using WhileLagoon.Application.Response;
using WhileLagoon.Domain.Entity;

namespace WhileLagoon.Application.Feature.ShopFeature.Command.DeleteShop
{
    public class DeleteShopCommandHandler(
        IShopRepository shopRepository
    ) : IRequestHandler<DeleteShopCommand, BaseResponse>
    {
        private readonly IShopRepository _shopRepository = shopRepository;
        public async Task<BaseResponse> Handle(DeleteShopCommand request, CancellationToken cancellationToken)
        {
            Shop foundShop = await _shopRepository.GetByIdAsync(request.ShopId)
                ?? throw new NotFoundException("Shop not found!");

            if (
                foundShop.ShopOwner.Contains(request.User.Id.ToString())
                && request.User.Role == Domain.Enum.Role.USER
            )
                throw new BadRequestException("Not permission to perform this action!");

            await _shopRepository.DeleteAsync(foundShop);

            BaseResponse baseResponse = new()
            {
                IsSuccess = true,
                IsError = false,
            };

            return baseResponse;

        }
    }
}
