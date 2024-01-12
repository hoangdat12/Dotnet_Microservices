using MediatR;
using Microsoft.Extensions.Configuration;
using WhileLagoon.Application.Constant;
using WhileLagoon.Application.Contract.Client;
using WhileLagoon.Application.Contract.Repository;
using WhileLagoon.Application.Contract.Service;
using WhileLagoon.Application.Event;
using WhileLagoon.Application.Exceptions;
using WhileLagoon.Application.Model;
using WhileLagoon.Application.Response;
using WhileLagoon.Application.Ultil;
using WhileLagoon.Domain.Constant;
using WhileLagoon.Domain.Entity;

namespace WhileLagoon.Application.Feature.AuthFeature.Command.ForgotPassword
{
    public class ForgotPasswordCommandHandler
    (
        IRabbitMqClient rabbitMqClient,
        IUserRepository userRepository,
        IConfiguration configuration,
        IRedisService redisService
    ) : IRequestHandler<ForgotPasswordCommand, BaseResponse>
    {
        private readonly IRabbitMqClient _rabbitMqClient = rabbitMqClient;
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IConfiguration _configuration = configuration;
        private readonly IRedisService _redisService = redisService;
        private readonly int _expireTime = 15;
        
        public async Task<BaseResponse> Handle(
            ForgotPasswordCommand request, 
            CancellationToken cancellationToken
        )
        {
            User foundUser = await _userRepository.GetByEmailAsync(request.Email)
                ?? throw new NotFoundException("User not found!");

            string token = GenerateToken.Generate();

            string key = $"{RedisConstant.FORGOT_PASSWORD}{token}";
            TimeSpan expireIn = TimeSpan.FromMinutes(_expireTime);
            await _redisService.SetStringAsync(key, foundUser.Email, expireIn);

            string link = $"{_configuration.GetSection(AppSetting.BaseUrl).Value}/confirm/email/{token}";

            RabbitMQConfiguration rabbitMQConfiguration = new();
            _configuration.GetSection(AppSetting.RabbitMQConfiguration).Bind(rabbitMQConfiguration);
            NotificationEvent notification = new()
            {
                Type = "email",
                UserName = $"{foundUser.FirstName} {foundUser.LastName}",
                Title = EmailType.ForgotPassword,
                Name = foundUser.Email,
                Content = link
            };
            _rabbitMqClient.PublishMessage<INotificationEvent>(
                rabbitMQConfiguration.NotificationExchangeName, 
                "notify",
                notification
            );
            
            return new BaseResponse() 
            {
                IsError = false,
                IsSuccess = true,
                Message = "Successfully"
            };
        }
    }
}