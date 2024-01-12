using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Filters;
using ProductService.API.Common;
using ProductService.Application.Constant;
using ProductService.Application.Exceptions;

namespace ProductService.API.Annotation
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
    public class AdminAuthorization : Attribute, IAsyncActionFilter
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
            if (role == Role.USER.ToString()) 
                throw new ForbiddenException("Not permission to perform this action!");
            await next();
        }
    }
}