

using MediatR;
using ProductService.Application.Constant;
using ProductService.Application.Contract.Infrastructure;
using ProductService.Application.Contract.Persistant;
using ProductService.Application.Dto.Discount;
using ProductService.Application.Exceptions;
using ProductService.Domain.Entity;
using ShopGRPCService;

namespace ProductService.Application.Feature.DiscountFeature.Command.UpdateDiscount
{
    public class UpdateDiscountCommandHandler(
        IDiscountRepository discountRepository,
        IProductRepository productRepository,
        IShopGRPCClient shopGRPCClient
    ) : IRequestHandler<UpdateDiscountCommand, Discount>
    {
        private readonly IDiscountRepository _discountRepository = discountRepository;
        private readonly IProductRepository _productRepository = productRepository;
        private readonly IShopGRPCClient _shopGRPCClient = shopGRPCClient;

        public async Task<Discount> Handle(UpdateDiscountCommand request, CancellationToken cancellationToken)
        {
            UpdateDiscountReq updateInfor = request.UpdateDiscountReq;

            Discount foundDiscount = await _discountRepository.GetByIdAsync(updateInfor.DiscountId)
                ?? throw new NotFoundException("Discount not found!");

            if (foundDiscount.DiscountShopId.Equals(null))
            {
                GetShopRes foundShop = await _shopGRPCClient.GetShopAsync(updateInfor.ToString())
                    ?? throw new Exception("Server error!");

                if (
                    foundShop != null && !foundShop.ShopName.Contains(request.User.UserId.ToString())
                )
                    throw new ForbiddenException("Not permission!");
            }

            if (foundDiscount.DiscountShopId.Equals(null) && request.User.Role == Role.USER.ToString())
                throw new ForbiddenException("Not permission!");    

            foundDiscount.DiscountName = updateInfor.DiscountName ?? foundDiscount.DiscountName;
            foundDiscount.DiscountDescription = updateInfor.DiscountDescription ?? foundDiscount.DiscountDescription;
            foundDiscount.DiscountType = !updateInfor.DiscountType.Equals(null) ? updateInfor.DiscountType : foundDiscount.DiscountType;
            foundDiscount.ReducePriceType = !updateInfor.ReducePriceType.Equals(null) ? updateInfor.ReducePriceType : foundDiscount.ReducePriceType;
            foundDiscount.DiscountValue = !updateInfor.DiscountValue.Equals(null) ? updateInfor.DiscountValue : foundDiscount.DiscountValue;
            foundDiscount.DiscountStartDate = !updateInfor.DiscountStartDate.Equals(null) ? updateInfor.DiscountStartDate : foundDiscount.DiscountStartDate;
            foundDiscount.DiscountEndDate = !updateInfor.DiscountEndDate.Equals(null) ? updateInfor.DiscountEndDate : foundDiscount.DiscountEndDate;
            foundDiscount.DiscountMaxUses = !updateInfor.DiscountMaxUses.Equals(null) ? updateInfor.DiscountMaxUses : foundDiscount.DiscountMaxUses;
            foundDiscount.DiscountMaxUsesPerUser = !updateInfor.DiscountMaxUsesPerUser.Equals(null) ? updateInfor.DiscountMaxUsesPerUser : foundDiscount.DiscountMaxUsesPerUser;
            foundDiscount.DiscountMinOrderValue = updateInfor.DiscountMinOrderValue ?? foundDiscount.DiscountMinOrderValue;
            foundDiscount.DiscountMaxOrderValue = updateInfor.DiscountMaxOrderValue ?? foundDiscount.DiscountMaxOrderValue;
            foundDiscount.DiscountAppliesTo = !updateInfor.DiscountAppliesTo.Equals(null) ? updateInfor.DiscountAppliesTo : foundDiscount.DiscountAppliesTo;
            foundDiscount.DiscountProductIds = updateInfor.DiscountProductIds ?? foundDiscount.DiscountProductIds;

            await _discountRepository.UpdateAsync(foundDiscount);

            return foundDiscount;
        }
    }
}