
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using WhileLagoon.Application.Constant;
using WhileLagoon.Application.Contract.Client;
using WhileLagoon.Application.Contract.Repository;
using WhileLagoon.Application.Contract.Service;
using WhileLagoon.Application.Event;
using WhileLagoon.Application.Exceptions;
using WhileLagoon.Application.Model;
using WhileLagoon.Application.Ultil;
using WhileLagoon.Domain.Constant;
using WhileLagoon.Domain.Entity;

namespace WhileLagoon.Application.Feature.AuthFeature.Command.Register
{
    public class RegisterCommandHandler(
        IUserRepository userRepository,
        IMapper mapper,
        IRedisService redisService,
        IRabbitMqClient rabbitMqClient,
        IConfiguration configuration
    ) : IRequestHandler<RegisterCommand, User>
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IMapper _mapper = mapper;
        private readonly IRedisService _redisService = redisService;
        private readonly IRabbitMqClient _rabbitMqClient = rabbitMqClient;
        private readonly IConfiguration _configuration = configuration;
        private readonly int _expireTime = 15;

        public async Task<User> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            User foundUser = await _userRepository.GetByEmailAsync(request.Email);
            if (foundUser is not null) throw new BadRequestException("User has already existed in system!");

            string salt = BCrypt.Net.BCrypt.GenerateSalt();
            string hashPassword = BCrypt.Net.BCrypt.HashPassword(request.Password, salt);

            User user = _mapper.Map<User>(request);
            user.Password = hashPassword;

            // Generate Token
            string token = GenerateToken.Generate();
            string link = $"{_configuration.GetSection(AppSetting.BaseUrl).Value}/active/{token}";

            // Save in Redis
            string key = $"{RedisConstant.ACTIVE_ACCOUNT}{token}";
            TimeSpan expireIn = TimeSpan.FromMinutes(_expireTime);
            await _redisService.SetStringAsync(key, user.Email, expireIn);

            // Send Mail
            RabbitMQConfiguration rabbitMQConfiguration = new();
            _configuration.GetSection(AppSetting.RabbitMQConfiguration).Bind(rabbitMQConfiguration);
            NotificationEvent notification = new()
            {
                Type = "email",
                UserName = $"{user.FirstName} {user.LastName}",
                Title = EmailType.ActiveAccount,
                Name = user.Email,
                Content = link
            };
            _rabbitMqClient.PublishMessage<INotificationEvent>(
                rabbitMQConfiguration.NotificationExchangeName, 
                "notify",
                notification
            );

            return await _userRepository.CreateAsync(user);
        }
    }
}
