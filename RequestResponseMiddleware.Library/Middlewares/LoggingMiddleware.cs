using Microsoft.AspNetCore.Http;

namespace RequestResponseMiddleware.Library.Middlewares
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public async Task Invoke(HttpContext context)
        {


            //Request
            await _next(context); //Executing
            //Response
        }
    }
}
