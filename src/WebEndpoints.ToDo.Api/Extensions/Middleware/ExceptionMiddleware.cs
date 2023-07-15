using Application.Common.Exceptions;
using ToDo.Domain.Common;
using Microsoft.IdentityModel.Tokens;
using System.Net;
using System.Text.Json;

namespace WebEndpoints.ToDo.Api.Extensions.Middleware
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
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            ErrorDetails result;

            if (exception is ValidationException)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                var list = new List<Error>();
                var validationError = (ValidationException)exception;
                if (!string.IsNullOrEmpty(validationError.Error))
                {
                    var errors= validationError.Error.Split(new char[] { ',' }).ToList();
                    foreach (var item in errors)
                    {
                        list.Add(new Error(item));
                    }
                }
                result = new ErrorDetails(
                    context.Response.StatusCode,
                    exception.Message,
                    context.Request.Path,
                list
                );
            }
            else if (exception is NotFoundException)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                result = new ErrorDetails(
                    400,
                    exception.Message,
                    context.Request.Path
                );
            }
            else
            {
                result = new ErrorDetails(
                    context.Response.StatusCode,
                    "System Error",
                    context.Request.Path,
                    new List<Error> { new Error(exception.GetBaseException().Message) }
                );
            }

            return context.Response.WriteAsync(JsonSerializer.Serialize(result, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            }));
        }
    }
}
