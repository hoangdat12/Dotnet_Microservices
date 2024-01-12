using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Configuration;
using WhileLagoon.Application.Constant;
using WhileLagoon.Application.Contract.Service;
using WhileLagoon.Application.Exceptions;
using WhileLagoon.Application.Response;
using WhileLagoon.Application.Ultil;
using WhileLagoon.Domain.Constant;

namespace WhileLagoon.Application.Feature.AuthFeature.Command.ConfirmEmail
{
    public class ConfirmEmailCommandHandler
    (
        IRedisService redisService,
        IConfiguration configuration
    ) : IRequestHandler<ConfirmEmailCommand, BaseResponse>
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly IRedisService _redisService = redisService;
        private readonly int _expireTime = 15;
        
        public async Task<BaseResponse> Handle(
            ConfirmEmailCommand request, 
            CancellationToken cancellationToken
        )
        {
            string key = $"{RedisConstant.FORGOT_PASSWORD}{request.Token}";
            string userEmail = await _redisService.GetStringAsync(key);
                
            if (userEmail.Equals(null)) {
                throw new BadRequestException("Invalid Token!");
            } else {
                _redisService.DeleteString(key);
            }

            string token = GenerateToken.Generate();

            string secondKey = $"{RedisConstant.RESET_PASSWORD}{token}";
            TimeSpan expireIn = TimeSpan.FromMinutes(_expireTime);
            await _redisService.SetStringAsync(secondKey, _expireTime, expireIn);

            string redirectLink = $"{_configuration.GetSection(AppSetting.BaseUrl).Value}/reset-password/{token}";

            return new BaseResponse()
            {
                IsError = false,
                IsSuccess = true,
                Message = redirectLink
            };
        }
    }
}