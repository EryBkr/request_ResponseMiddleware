using Microsoft.AspNetCore.Http;
using RequestResponseMiddleware.Library.Interfaces;
using RequestResponseMiddleware.Library.Models;

namespace RequestResponseMiddleware.Library.Middlewares
{
    public class HandlerRequestResponseLoggingMiddleware : BaseRequestResponseMiddleware
    {
        private readonly Func<RequestResponseContext, Task> reqResHandler;
        
        public HandlerRequestResponseLoggingMiddleware(RequestDelegate next, Func<RequestResponseContext, Task> reqResHandler, ILogWriter _logWriter) : base(next, _logWriter)
        {
            this.reqResHandler = reqResHandler;
        }

        public async Task Invoke(HttpContext context)
        {
            //Oluşan log datalarını aldım
            var reqResContext = await base.InvokeBaseMiddleware(context);

            await reqResHandler.Invoke(reqResContext);


        }
    }
}
