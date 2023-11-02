using System;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace WebApi.Middlewares
{
    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var watch = Stopwatch.StartNew();

            try
            {
                string message = "[Request] Http " + context.Request.Method + " - " + context.Request.Path;
                LogMessage(message);

                await _next(context);
                watch.Stop();

                message = "[Response] Http " + context.Request.Method + " - " + context.Request.Path + " - Responded " + context.Response.StatusCode + " in " + watch.Elapsed.TotalMilliseconds + " ms.";
                LogMessage(message);
            }
            catch (Exception ex)
            {
                watch.Stop();
                await HandleException(context, ex, watch);
            }
        }

        private async Task HandleException(HttpContext context, Exception ex, Stopwatch watch)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            string message = "[Error]    HTTP " + context.Request.Method + " - " + context.Response.StatusCode + " Error Message : " + ex.Message + " in " + watch.Elapsed.TotalMilliseconds + " ms";
            LogMessage(message);

            var result = JsonConvert.SerializeObject(new { error = ex.Message }, Formatting.None);

            await context.Response.WriteAsync(result);
        }

        private void LogMessage(string message)
        {
             Console.WriteLine(message);
        }
    }

    public static class CustomExceptionMiddlewareExtension
    {
        public static IApplicationBuilder UseCustomExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionMiddleware>();
        }
    }
}
