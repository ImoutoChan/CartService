using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace CartService.Infrastructure
{
    // todo clean up
    public class ErrorLoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine($"The following error happened: {e.Message}");
                throw;
            }
        }
    }
}