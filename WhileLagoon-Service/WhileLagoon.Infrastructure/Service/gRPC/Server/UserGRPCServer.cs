using System.Security.Claims;
using System.Text.Json;
using Amazon.S3.Model;
using AutoMapper;
using Grpc.Core;
using UserGRPCService;
using WhileLagoon.Application.Contract.Repository;
using WhileLagoon.Application.Contract.Service;
using WhileLagoon.Application.Exceptions;
using WhileLagoon.Domain.Entity;

namespace WhileLagoon.Infrastructure.Service
{
    public class UserGRPCServer
    (
        IUserRepository userRepository,
        IMapper mapper,
        IJwtService jwtService
    ): UserGRPC.UserGRPCBase
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IMapper _mapper = mapper;
        private readonly IJwtService _jwtService = jwtService;

        public override async Task<GetUserRes> GetUser(GetUserReq request, ServerCallContext context)
        {
            User foundUser = await _userRepository.GetByIdAsync(new Guid(request.UserId));
            return _mapper.Map<GetUserRes>(foundUser);
        }

        public override async Task<VerifyAccessTokenRes> VerifyAccessToken(
            VerifyAccessTokenReq request, ServerCallContext context
        )
        {
            ClaimsPrincipal principal;
            VerifyAccessTokenRes response = new();

            try {
                principal = await _jwtService.VerifyAccessToken(new Guid(request.UserId), request.Token) 
                ?? throw new ForbiddenException("Invalid token");
            } catch (Exception)
            {
                response.UserId = "";
                response.Email = "";
                response.Role = "";
                response.IsValid = false;
                return response;
            }

            User user = _mapper.Map<User>(principal);

            response.UserId = user.Id.ToString();
            response.Email = user.Email;
            response.Role = user.Role.ToString();
            response.IsValid = true;

            return response;
        }
    }
}