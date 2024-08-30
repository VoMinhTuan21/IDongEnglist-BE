using IDonEnglist.Application.Exceptions;
using Newtonsoft.Json;
using System.Net;

namespace IDonEnglist.API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }
        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
            string result = JsonConvert.SerializeObject(new ErrorDetails { ErrorMessage = exception.Message, ErrorType = "Failure" });

            switch (exception)
            {
                case BadRequestException badRequestException:
                    statusCode = HttpStatusCode.BadRequest;
                    result = JsonConvert.SerializeObject(new ErrorDetails { ErrorMessage = badRequestException.Message, ErrorType = "Failure" });
                    break;
                case NotFoundException notFound:
                    statusCode = HttpStatusCode.NotFound;
                    result = JsonConvert.SerializeObject(new ErrorDetails { ErrorMessage = notFound.Message, ErrorType = "Failure" });
                    break;
                case ValidatorException validatorException:
                    statusCode = HttpStatusCode.BadRequest;
                    result = JsonConvert.SerializeObject(new ErrorDetails { ErrorMessage = string.Join(",", validatorException.Errors), ErrorType = "Failure" });
                    break;
                case ForbiddenException forbidden:
                    statusCode = HttpStatusCode.Forbidden;
                    result = JsonConvert.SerializeObject(new ErrorDetails { ErrorMessage = forbidden.Message, ErrorType = "Failure" });
                    break;
                default:
                    break;
            }

            context.Response.StatusCode = (int)statusCode;
            return context.Response.WriteAsync(result);
        }
    }

    public class ErrorDetails
    {
        public string ErrorType { get; set; }
        public string ErrorMessage { get; set; }
    }
}
