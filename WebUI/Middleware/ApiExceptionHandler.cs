using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace JobControl.WebUI.Middleware
{
    public class ApiExceptionHandlerMiddleware    
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;


        public ApiExceptionHandlerMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger<ApiExceptionHandlerMiddleware>();
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected exception has been thrown.");
                // Once response has been started the only thing left to do is default processing.
                if (context.Response.HasStarted) 
                {
                    _logger.LogError("Unable to handle exception in middleware.");
                    throw;
                }
                context.Response.StatusCode = 500;
                context.Response.Headers.Clear();
                context.Response.Headers.AppendCommaSeparatedValues("Content-Type", "application/json");
                context.Response.Headers.AppendCommaSeparatedValues("Content-Language", "en-US");
                using (var writer = new StreamWriter(context.Response.Body, Encoding.UTF8, bufferSize: 1024, leaveOpen: true))
                {
                    writer.Write("{\"message\":[");
                    writer.Write("\"An unexpected error has occured and been logged.\",");
                    writer.Write("\"If the problem persist please contact help desk.\"]");
                }
            }
            return;
        }
    }
}

namespace Microsoft.AspNetCore.Builder
{
    public static class ApiExceptionHandlerExtensions
    {
        public static IApplicationBuilder UseApiExceptionHandler(this IApplicationBuilder app)
        {
            return app.UseMiddleware<JobControl.WebUI.Middleware.ApiExceptionHandlerMiddleware>();
        }
    }
}