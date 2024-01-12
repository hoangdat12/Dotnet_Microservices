using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Filters;
using WhileLagoon.Application.Contract.Service;
using WhileLagoon.Application.Exceptions;

namespace WhileLaggon.API.Common
{
    public static class Authorization
    {
        public static async Task<ClaimsPrincipal> OnActionExecutionAsync(ActionExecutingContext context)
        {
            var token = (context.HttpContext.Request.Headers.Authorization.FirstOrDefault()?.Split(" ").Last()) 
                ?? throw new UnAuthorizationException("UnAuthorization!");
            string? userId = context.HttpContext.Request.Headers["x-client-id"];
            if (userId == null) throw new BadRequestException("Missing request value");
            IJwtService jwtService = context.HttpContext.RequestServices
                .GetRequiredService<IJwtService>();

            ClaimsPrincipal principal = await jwtService.VerifyAccessToken(new Guid(userId), token) 
                ?? throw new ForbiddenException("Invalid token");

            return principal;
        }
    }
}