using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ProductService.Application.Contract.Infrastructure;
using ProductService.Application.Dto.AppSetting;

namespace ProductService.API.Annotation
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
    public class Cache(int timeToLiveSeconds = 300) : Attribute, IAsyncActionFilter
    {
        private readonly int _timeToLiveSeconds = timeToLiveSeconds;
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var configuration = context.HttpContext.RequestServices
                                    .GetRequiredService<RedisConfiguration>();
            if (!configuration.Enable) {
                await next();
                return;
            }

            var cacheService = context.HttpContext.RequestServices
                .GetRequiredService<IRedisService>();

            string cacheKey = GenerateCacheKeyFromRequest(context.HttpContext.Request);
            string cacheResponse = await cacheService.GetCacheAsync(cacheKey);

            if (!string.IsNullOrEmpty(cacheKey)) {
                var contentResult = new ContentResult {
                    Content = cacheResponse,
                    ContentType = "application/json",
                    StatusCode = 200
                };

                context.Result = contentResult;
                return;
            } 

            var executedContext = await next();
            if (executedContext.Result is OkObjectResult okObjectResult) {
                await cacheService.SetCacheAsync(
                    cacheKey, 
                    okObjectResult.Value, 
                    TimeSpan.FromSeconds(_timeToLiveSeconds)
                );
            }
        }

        private static string GenerateCacheKeyFromRequest(HttpRequest request) 
        {
            var keyBuilder = new StringBuilder();
            keyBuilder.Append($"{request.Path}");

            foreach (var (key, value) in request.Query.OrderBy(x => x.Key))
            {
                keyBuilder.Append($":{key}-{value}");
            }
            return keyBuilder.ToString();
        }
    }
}