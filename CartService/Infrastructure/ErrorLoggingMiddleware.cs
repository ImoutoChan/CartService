using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace CartService.Infrastructure
{
    public class ErrorLoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, ILogger<ErrorLoggingMiddleware> logger)
        {
            try
            {
                await _next(context);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An exception occured while running ASPNET CORE pipeline.");
                throw;
            }
        }
    }
}