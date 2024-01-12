using MediatR;
using WhileLagoon.Application.Contract.Repository;
using WhileLagoon.Application.Exceptions;
using WhileLagoon.Domain.Entity;

namespace WhileLagoon.Application.Feature.UserFeature.Query.GetUserDetail
{
    public class GetUserDetailCommandHandler(IUserRepository userRepository) : IRequestHandler<GetUserDetailCommand, User>
    {
        private readonly IUserRepository _userRepository = userRepository;
        public async Task<User> Handle(GetUserDetailCommand request, CancellationToken cancellationToken)
        {
            User foundUser = await _userRepository.GetByIdAsync(request.UserId) 
                ?? throw new NotFoundException("User not found!");
            return foundUser;
        }
    }
}