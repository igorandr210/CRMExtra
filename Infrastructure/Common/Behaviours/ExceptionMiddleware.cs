using System;
using System.Net;
using System.Threading.Tasks;
using Domain.Common;
using Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Infrastructure.Common.Behaviours
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
            catch (NotAllowedException ex)
            {
                await HandleForbiddenExceptionAsync(httpContext, ex);
            }
            catch (NotFoundException ex)
            {
                await HandleNotFoundExceptionAsync(httpContext, ex);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleForbiddenExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int) HttpStatusCode.Forbidden;

            var responseObject = new ResponseModelBase<object>(null)
            {
                ErrorMessage = exception.Message
            };
            
            return context.Response.WriteAsync(JsonConvert.SerializeObject(responseObject));
        }

        private Task HandleNotFoundExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.NotFound;

            var responseObject = new ResponseModelBase<object>(null)
            {
                ErrorMessage = exception.Message
            };

            return context.Response.WriteAsync(JsonConvert.SerializeObject(responseObject));
        }
        
        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;

            var message = $"{exception.Message} \n";

            message += exception.InnerException?.Message ?? "";

            var responseObject = new ResponseModelBase<object>(null)
            {
                ErrorMessage = message
            };

            return context.Response.WriteAsync(JsonConvert.SerializeObject(responseObject));
        }
    }
}