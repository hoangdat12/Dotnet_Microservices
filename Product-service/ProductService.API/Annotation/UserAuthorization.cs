using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Filters;
using ProductService.API.Common;

namespace ProductService.API.Annotation
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
    public class UserAuthorization : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(
            ActionExecutingContext context, 
            ActionExecutionDelegate next
        )
        {
            ClaimsPrincipal principal = await Authorization.OnActionExecutionAsync(context);
            context.HttpContext.User = principal;

            await next();
        }
    }
}