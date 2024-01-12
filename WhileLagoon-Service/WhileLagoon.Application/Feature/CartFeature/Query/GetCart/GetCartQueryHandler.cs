using MediatR;
using WhileLagoon.Application.Contract.Repository;
using WhileLagoon.Application.Exceptions;
using WhileLagoon.Domain.Entity;

namespace WhileLagoon.Application.Feature.CartFeature.Query.GetCart
{
    public class GetCartQueryHandler(
        ICartRepository cartRepository,
        ICartProductRepository cartProductRepository
    ) : IRequestHandler<GetCartQuery, Cart>
    {
        private readonly ICartProductRepository _cartProductRepository = cartProductRepository;
        private readonly ICartRepository _cartRepository = cartRepository;
        public async Task<Cart> Handle(GetCartQuery request, CancellationToken cancellationToken)
        {
            Cart foundCart = await _cartRepository.GetByUserIdAsync(request.UserId) 
                ?? throw new NotFoundException("User not found!");

            if (foundCart.UserId == request.User.Id) {
                List<CartProduct> cartProducts = 
                    await _cartProductRepository.GetProductsAsync(foundCart.Id, request.Page, request.Limit);
                foundCart.Products = cartProducts;

                return foundCart;
            }
            else throw new ForbiddenException("UnAuthorization!");
        }
    }
}