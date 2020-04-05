using Core.Exceptions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http.Filters;

namespace WhiteLabelAPI.WebAPI.Midlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate next; 
        public ErrorHandlingMiddleware(RequestDelegate next) { this.next = next; }
        public async Task Invoke(HttpContext context /* other dependencies */)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError;
            //// 500 if unexpected 
            if (exception is CustomNotFoundException) code = HttpStatusCode.NotFound; 
            else if (exception is CustomUnauthorizedException) code = HttpStatusCode.Unauthorized; 
            else if (exception is CustomBadRequestException) code = HttpStatusCode.BadRequest; 

            var result = JsonConvert.SerializeObject(new { error = exception.Message }); 
            context.Response.ContentType = "application/json"; context.Response.StatusCode = (int)code; 
            return context.Response.WriteAsync(result);
        }
    }
}