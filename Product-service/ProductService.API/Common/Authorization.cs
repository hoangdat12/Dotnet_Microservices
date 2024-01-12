using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Filters;
using ProductService.Application.Contract.Infrastructure;
using ProductService.Application.Exceptions;
using ProductService.Infrustructure.Service;
using UserGRPCService;

namespace ProductService.API.Common
{
    public class Authorization
    {
        public static async Task<ClaimsPrincipal> OnActionExecutionAsync(
            ActionExecutingContext context
        )
        {
            var token = (context.HttpContext.Request.Headers.Authorization
                .FirstOrDefault()?
                .Split(" ")
                .Last()) 
                ?? throw new UnAuthorizationException("UnAuthorization!");

            string? userId = context.HttpContext.Request.Headers["x-client-id"];
            if (userId == null) throw new BadRequestException("Missing request value");

            var _userGRPCClient = context.HttpContext.RequestServices
                .GetRequiredService<IUserGRPCClient>();

            VerifyAccessTokenRes decode = await _userGRPCClient
                .VerifyAccessTokenAsync(userId, token);

            if (!decode.IsValid) throw new ForbiddenException("Invalid token!");

            var customClaims = new List<Claim>
            {
                new("role", decode.Role.ToString()),
                new("userId", decode.UserId),
                new("email", decode.Email)
            };

            ClaimsPrincipal principal = new();
            var identity = principal.Identity as ClaimsIdentity;
            identity?.AddClaims(customClaims);

            return principal;
        }
    }
}