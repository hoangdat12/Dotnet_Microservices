using MediatR;
using WhileLagoon.Application.Contract.Service;
using WhileLagoon.Application.Exceptions;
using WhileLagoon.Domain.Entity;

namespace WhileLagoon.Application.Feature.AuthFeature.Command.RefreshToken
{
    public class RefreshTokenCommandHandler(
        IJwtService jwtService
    ) : IRequestHandler<RefreshTokenCommand, string>
    {
        private readonly IJwtService _jwtService = jwtService;
        public async Task<string> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            User user = await _jwtService.VerifyRefreshToken(request.UserId, request.RefreshToken)
                ?? throw new ForbiddenException("Invalid Token!");

            string accessToken = _jwtService.GenerateAccessToken(user);
            return accessToken;
        }
    }
}