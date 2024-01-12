using MediatR;
using WhileLagoon.Application.Contract.Repository;
using WhileLagoon.Application.Contract.Service;
using WhileLagoon.Application.Exceptions;
using WhileLagoon.Domain.Constant;
using WhileLagoon.Domain.Entity;

namespace WhileLagoon.Application.Feature.AuthFeature.Command.ActiveAccount
{
    public class ActiveAccountCommandHandler(
        IUserRepository userRepository,
        ICartRepository cartRepository,
        IRedisService redisService
     ) : IRequestHandler<ActiveAccountCommand, Unit>
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly ICartRepository _cartRepository = cartRepository;
        private readonly IRedisService _redisService = redisService;
        public async Task<Unit> Handle(ActiveAccountCommand request, CancellationToken cancellationToken)
        {
            string key = $"{RedisConstant.ACTIVE_ACCOUNT}{request.Token}";
            string userEmail = await _redisService.GetStringAsync(key);

            if (string.IsNullOrEmpty(userEmail))
                throw new BadRequestException("Invalid token for active account!");

            User foundUser = await _userRepository.GetByEmailAsync(userEmail) 
                 ?? throw new NotFoundException("User not found!");

            foundUser.Status = Domain.Enum.UserStatus.ACTIVE;

            await _userRepository.UpdateAsync(foundUser);

            // Delete 
            _redisService.DeleteString(key);

            Cart cart = new() 
            {
                UserId = foundUser.Id,
                User = foundUser
            };
            await _cartRepository.CreateAsync(cart);

            return Unit.Value;
        }
    }
}
