using System.Net;
using WhileLagoon.Application.Exceptions;
using WhileLaggon.API.Model;

namespace WhileLaggon.API.Middleware
{
    public class ErrorHandlerMiddleware(RequestDelegate next)
    {
        private readonly RequestDelegate _next = next;

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                await HandlerException(httpContext, e);
            }
        }

        private async Task HandlerException(HttpContext httpContext, Exception e)
        {
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
            CustomValidationProblemDetail problem;

            switch (e)
            {
                case NotFoundException notFoundException:
                    statusCode = HttpStatusCode.NotFound;
                    problem = new CustomValidationProblemDetail
                    {
                        Title = notFoundException.Message,
                        Status = (int)statusCode,
                        Detail = notFoundException.InnerException?.Message,
                        Type = nameof(NotFoundException),
                    };
                    break;
                case BadRequestException badRequestException:
                    statusCode = HttpStatusCode.BadRequest;
                    problem = new CustomValidationProblemDetail
                    {
                        Title = badRequestException.Message,
                        Status = (int)statusCode,
                        Detail = badRequestException.InnerException?.Message,
                        Type = nameof(BadRequestException),
                    };
                    break;
                case ForbiddenException forbiddenException:
                    statusCode = HttpStatusCode.Forbidden;
                    problem = new CustomValidationProblemDetail
                    {
                        Title = forbiddenException.Message,
                        Status = (int)statusCode,
                        Detail = forbiddenException.InnerException?.Message,
                        Type = nameof(ForbiddenException),
                    };
                    break;
                case UnAuthorizationException unAuthorizationException:
                    statusCode = HttpStatusCode.Unauthorized;
                    problem = new CustomValidationProblemDetail
                    {
                        Title = unAuthorizationException.Message,
                        Status = (int)statusCode,
                        Detail = unAuthorizationException.InnerException?.Message,
                        Type = nameof(UnAuthorizationException),
                    };
                    break;
                default:
                    problem = new CustomValidationProblemDetail
                    {
                        Title = e.Message,
                        Status = (int)statusCode,
                        Detail = e.InnerException?.Message,
                        Type = nameof(Exception),
                    };
                    break;
            }

            httpContext.Response.StatusCode = (int)statusCode;
            await httpContext.Response.WriteAsJsonAsync(problem);
        }
    }
}
