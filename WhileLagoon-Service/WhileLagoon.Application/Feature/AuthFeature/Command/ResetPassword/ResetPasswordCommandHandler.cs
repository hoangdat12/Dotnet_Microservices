

using BCrypt.Net;
using MediatR;
using WhileLagoon.Application.Contract.Repository;
using WhileLagoon.Application.Contract.Service;
using WhileLagoon.Application.Exceptions;
using WhileLagoon.Application.Response;
using WhileLagoon.Domain.Constant;
using WhileLagoon.Domain.Entity;

namespace WhileLagoon.Application.Feature.AuthFeature.Command.ResetPassword
{
    public class ResetPasswordCommandHandler
    (
        IRedisService redisService,
        IUserRepository userRepository
    ) : IRequestHandler<ResetPasswordCommand, BaseResponse>
    {
        private readonly IRedisService _redisService = redisService;
        private readonly IUserRepository _userRepository = userRepository; 

        public async Task<BaseResponse> Handle(
            ResetPasswordCommand request, 
            CancellationToken cancellationToken
        )
        {
            string key = $"{RedisConstant.RESET_PASSWORD}{request.Token}";
            string userEmail = await _redisService.GetStringAsync(key);
            if (userEmail.Equals(null)) throw new BadRequestException("Invalid token!");
            else _redisService.DeleteString(key);

            User foundUser = await _userRepository.GetByEmailAsync(userEmail)
                ?? throw new BadRequestException("Invalid token!");

            string salt = BCrypt.Net.BCrypt.GenerateSalt(10);            
            string hashPassword = BCrypt.Net.BCrypt.HashPassword(request.Password, salt);  

            foundUser.Password = hashPassword;

            await _userRepository.UpdateAsync(foundUser);  

            return new BaseResponse()
            {
                IsError = false,
                IsSuccess = true,
                Message = "Successfully!"
            };
        }
    }
}