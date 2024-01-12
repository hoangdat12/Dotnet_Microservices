
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Filters;
using WhileLagoon.Application.Exceptions;
using WhileLagoon.Domain.Enum;
using WhileLaggon.API.Common;

namespace WhileLaggon.API.Annotation
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
    public class SuperAuthorization : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            ClaimsPrincipal principal = await Authorization.OnActionExecutionAsync(context);

            string? role = principal.FindFirstValue("role")?.ToString() 
                ?? throw new ForbiddenException("Invalid Token!");
            if (role != Role.SUPER.ToString()) 
                throw new ForbiddenException("Not permission to perform this action!");

            context.HttpContext.User = principal;

            await next();
        }
    }
}