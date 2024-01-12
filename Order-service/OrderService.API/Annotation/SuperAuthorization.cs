

using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Filters;
using OrderService.API.Common;
using OrderService.Application.Constant;
using OrderService.Application.Exceptions;

namespace OrderService.API.Annotation
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
    public class SuperAuthorization : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(
            ActionExecutingContext context, 
            ActionExecutionDelegate next
        )
        {
            ClaimsPrincipal principal = await Authorization.OnActionExecutionAsync(context);
            context.HttpContext.User = principal;

            string? role = principal.FindFirstValue("role")?.ToString() 
                ?? throw new ForbiddenException("Invalid Token!");
            if (role != Role.SUPER.ToString()) 
                throw new ForbiddenException("Not permission to perform this action!");

            await next();
        }
    }
}