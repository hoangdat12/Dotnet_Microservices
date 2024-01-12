using MediatR;
using WhileLagoon.Application.Contract.Repository;
using WhileLagoon.Application.Exceptions;
using WhileLagoon.Application.Response;
using WhileLagoon.Domain.Entity;

namespace WhileLagoon.Application.Feature.CartFeature.Command.ChangeQuantity
{
    public class ChangeQuantityCommandHandler
    (
        ICartRepository cartRepository,
        ICartProductRepository cartProductRepository
    ) : IRequestHandler<ChangeQuantityCommand, BaseResponse>
    {
        private readonly ICartRepository _cartRepository = cartRepository;
        private readonly ICartProductRepository _cartProductRepository = cartProductRepository;
        public async Task<BaseResponse> Handle(ChangeQuantityCommand request, CancellationToken cancellationToken)
        {
            Cart foundCart = await _cartRepository.GetByUserIdAsync(request.User.Id)
                ?? throw new NotFoundException("Cart not found!");
            
            CartProduct foundProduct = await _cartProductRepository.GetProductInCartAsync(request.Product.ProductId, foundCart.Id)
                ?? throw new NotFoundException("Product not found!");

            if (foundProduct.ProductQuantity + request.Product.ProductQuantity <= 0) {
                // Delete product from Cart
                await _cartProductRepository.DeleteAsync(foundProduct);
                return new BaseResponse()
                {
                    IsSuccess = true,
                    IsError = false
                };
            } 

            foundProduct.ProductQuantity += request.Product.ProductQuantity;

            await _cartProductRepository.UpdateAsync(foundProduct);

            return new BaseResponse()
            {
                IsSuccess = true,
                IsError = false
            };
        }
    }
}