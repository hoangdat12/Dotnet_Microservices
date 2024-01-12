
using MediatR;
using WhileLagoon.Application.Contract.Repository;
using WhileLagoon.Application.Exceptions;
using WhileLagoon.Domain.Entity;

namespace WhileLagoon.Application.Feature.CartFeature.Command.CreateCart
{
    public class CreateCardCommandHandler
    (
        IUserRepository userRepository,
        ICartRepository cartRepository
    ) : IRequestHandler<CreateCartCommand, Cart>
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly ICartRepository _cartRepository = cartRepository;
        public async Task<Cart> Handle(CreateCartCommand request, CancellationToken cancellationToken)
        {
            User foundUser = await _userRepository.GetByIdAsync(request.UserId)
                ?? throw new NotFoundException("User not found!");

            Cart cart = new()
            {
                UserId = foundUser.Id,
                User = foundUser
            };

            return await _cartRepository.CreateAsync(cart);
        }
    }
}