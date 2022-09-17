using Microsoft.AspNetCore.Http;
using System.Net.Http;

namespace RequestResponseMiddleware.Library.Middlewares
{
    class LoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public async Task Invoke(HttpContent context)
        {
            
        }
    }
}
