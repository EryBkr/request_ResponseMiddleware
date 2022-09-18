using Microsoft.AspNetCore.Http;
using Microsoft.IO;
using RequestResponseMiddleware.Library.Models;
using System.Diagnostics;
using System.IO;

namespace RequestResponseMiddleware.Library.Middlewares
{
    public class LoggingMiddleware : BaseRequestResponseMiddleware
    {
        public LoggingMiddleware(RequestDelegate next, RequestResponseOptions reqOptions) : base(next)
        {

        }
        public async Task Invoke(HttpContext context)
        {
            //Oluşan log datalarını aldım
            var reqResContext =await base.InvokeBaseMiddleware(context);
        }



    }
}
