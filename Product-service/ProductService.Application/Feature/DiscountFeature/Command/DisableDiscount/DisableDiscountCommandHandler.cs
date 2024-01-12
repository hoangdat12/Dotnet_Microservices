

using MediatR;
using ProductService.Application.Constant;
using ProductService.Application.Contract.Infrastructure;
using ProductService.Application.Contract.Persistant;
using ProductService.Application.Exceptions;
using ProductService.Application.Response;
using ProductService.Domain.Entity;
using ShopGRPCService;

namespace ProductService.Application.Feature.DiscountFeature.Command.DisableDiscount
{
    public class DisableDiscountCommandHandler
    (
        IDiscountRepository discountRepository,
        IShopGRPCClient shopGRPCClient
    ) : IRequestHandler<DisableDiscountCommand, BaseResponse>
    {
        private readonly IDiscountRepository _discountRepository = discountRepository;
        private readonly IShopGRPCClient _shopGRPCClient = shopGRPCClient;

        public async Task<BaseResponse> Handle(DisableDiscountCommand request, CancellationToken cancellationToken)
        {
            Discount foundDiscount = await _discountRepository.GetByIdAsync(request.DiscountId)
                ?? throw new NotFoundException("Discount not found!");
            
            if (foundDiscount.DiscountShopId.Equals(null))
            {
                GetShopRes foundShop = await _shopGRPCClient.GetShopAsync(foundDiscount.DiscountShopId.ToString())
                    ?? throw new Exception("Server error!");

                if (
                    foundShop != null && !foundShop.ShopName.Contains(request.User.UserId.ToString())
                )
                    throw new ForbiddenException("Not permission!");
            }

            if (foundDiscount.DiscountShopId.Equals(null) && request.User.Role == Role.USER.ToString())
                throw new ForbiddenException("Not permission!");       

            foundDiscount.DiscountIsActive = false;
            await _discountRepository.UpdateAsync(foundDiscount);

            return new BaseResponse()
            {
                IsError = false,
                IsSuccess = true,
                Message = "Disable discount successfully!"
            };
        }
    }
}