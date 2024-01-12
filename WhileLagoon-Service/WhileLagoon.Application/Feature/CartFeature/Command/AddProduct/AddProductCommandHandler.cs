using AutoMapper;
using MediatR;
using ProductService;
using WhileLagoon.Application.Contract.Repository;
using WhileLagoon.Application.Contract.Service;
using WhileLagoon.Application.Exceptions;
using WhileLagoon.Domain.Entity;

namespace WhileLagoon.Application.Feature.CartFeature.Command.AddProduct
{
    public class AddProductCommandHandler
    (
        ICartRepository cartRepository,
        ICartProductRepository cartProductRepository,
        IMapper mapper,
        IProductGRPCService productGRPCService
    ) : IRequestHandler<AddProductCommand, CartProduct>
    {
        private readonly ICartRepository _cartRepository = cartRepository;
        private readonly ICartProductRepository _cartProductRepository = cartProductRepository;
        private readonly IMapper _mapper = mapper;
        private readonly IProductGRPCService _productGRPCService = productGRPCService;

        public async Task<CartProduct> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            Cart foundCart = await _cartRepository.GetByUserIdAsync(request.User.Id)    
                ?? throw new BadRequestException("Cart not found!");

            GetProductRes res = await _productGRPCService.GetProduct(request.Product.ProductId.ToString());
            if (res.Equals(null)) throw new BadRequestException("Product not found!");

            CartProduct cartProduct = _mapper.Map<CartProduct>(request.Product);
            cartProduct.Cart = foundCart;
            cartProduct.CartId = foundCart.Id;

            return await _cartProductRepository.CreateAsync(cartProduct);
        }
    }
}