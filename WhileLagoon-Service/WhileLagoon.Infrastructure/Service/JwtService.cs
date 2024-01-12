using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json;
using WhileLagoon.Application.Constant;
using WhileLagoon.Application.Contract.Repository;
using WhileLagoon.Application.Contract.Service;
using WhileLagoon.Application.Dto;
using WhileLagoon.Application.Exceptions;
using WhileLagoon.Domain.Entity;

namespace WhileLagoon.Infrastructure.Service
{
    public class JwtService(
        IConfiguration configuration, 
        IUserRepository userRepository
    ) : IJwtService
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly double REFRESH_TOKEN_EXPRISE_IN = 1;
        private readonly IUserRepository _userRepository = userRepository;
        public string GenerateAccessToken(User user)
        {
            JwtOption jwtOption = new();
            _configuration.GetSection(AppSetting.JwtSettings).Bind(jwtOption);
            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(jwtOption.SecretKey)),
                SecurityAlgorithms.HmacSha256
            );

            var claims = new[]
            {
                new Claim("userId", user.Id.ToString()),
                new Claim("email", user.Email),
            };

            // Create a JWT token
            var token = new JwtSecurityToken(
                issuer: jwtOption.Issuer,
                audience: jwtOption.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(jwtOption.ExpireIn),
                signingCredentials: signingCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GenerateRefreshToken(User user)
        {
            JwtOption jwtOption = new();
            _configuration.GetSection(AppSetting.JwtSettings).Bind(jwtOption);
            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(jwtOption.SecretKey)),
                SecurityAlgorithms.HmacSha256
            );

            var claims = new[]
            {
                new Claim("userId", user.Id.ToString()),
                new Claim("email", user.Email),
            };

            // Create a JWT token
            var token = new JwtSecurityToken(
                issuer: jwtOption.Issuer,
                audience: jwtOption.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddDays(REFRESH_TOKEN_EXPRISE_IN),
                signingCredentials: signingCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string RefreshToken(User user, string token)
        {
            throw new NotImplementedException();
        }

        public async Task<ClaimsPrincipal> VerifyAccessToken(Guid userId, string token)
        {
            ValidateTokenData data = await ValidateToken(userId, token);
            return data.principal;
        }

        public async Task<User> VerifyRefreshToken(Guid userId, string token) {
            ValidateTokenData data = await ValidateToken(userId, token);
            return data.user;
        }

        private async Task<ValidateTokenData> ValidateToken(Guid userId, string token)
        {
            User foundUser = await _userRepository.GetByIdAsync(userId) 
                ?? throw new NotFoundException("User not found!");
            JwtOption jwtOption = new();
            _configuration.GetSection(AppSetting.JwtSettings).Bind(jwtOption);

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidIssuer = jwtOption.Issuer,
                ValidAudience = jwtOption.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(jwtOption.SecretKey)),
            };

            var handler = new JwtSecurityTokenHandler();
            
            try {
                ClaimsPrincipal principal = handler.ValidateToken(token, validationParameters, out _);

                if (principal.FindFirstValue("userId")?.ToString() != userId.ToString())
                    throw new BadRequestException("Invalid User Id");

                var customClaims = new List<Claim>
                {
                    new("role", foundUser.Role.ToString()),
                    new("email", foundUser.Email),
                };

                var identity = principal.Identity as ClaimsIdentity;
                identity?.AddClaims(customClaims);
                
                ValidateTokenData validateTokenData = new() 
                {
                    user = foundUser,
                    principal = principal
                };

                return validateTokenData;
            } catch(Exception e) {
                throw new ForbiddenException(e.Message);
            }
        }
    }
}
