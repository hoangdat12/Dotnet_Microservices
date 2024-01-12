using MediatR;
using WhileLagoon.Application.Contract.Repository;
using WhileLagoon.Application.Contract.Service;
using WhileLagoon.Application.Dto.Auth;
using WhileLagoon.Application.Exceptions;
using WhileLagoon.Domain.Entity;

namespace WhileLagoon.Application.Feature.AuthFeature.Command.Login
{
    public class LoginCommandHandler(
        IUserRepository userRepository,
        IJwtService jwtService
    ) : IRequestHandler<LoginCommand, AuthenticateRes>
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IJwtService _jwtService = jwtService;
        public async Task<AuthenticateRes> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            User foundUser = await _userRepository.GetByEmailAsync(request.Email) 
                ?? throw new BadRequestException("User not found!");
            bool validPassword = BCrypt.Net.BCrypt.Verify(request.Password, foundUser.Password);
            if (!validPassword) throw new UnAuthorizationException("Wrong password!");

            string accessToken =
                _jwtService.GenerateAccessToken(foundUser);
            string refreshToken =
                _jwtService.GenerateRefreshToken(foundUser);

            // Update or insert keytoken
            AuthenticateRes response = new()
            {
                User = foundUser,
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
            return response;
        }
    }
}
