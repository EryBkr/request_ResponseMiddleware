using Microsoft.AspNetCore.Http;
using Microsoft.IO;
using RequestResponseMiddleware.Library.Interfaces;
using RequestResponseMiddleware.Library.Models;
using System.Diagnostics;
using System.IO;

namespace RequestResponseMiddleware.Library.Middlewares
{
    public class LoggingMiddleware : BaseRequestResponseMiddleware
    {
        private readonly ILogWriter logWriter;

        public LoggingMiddleware(RequestDelegate next, ILogWriter _logWriter) : base(next, _logWriter)
        {
            logWriter = _logWriter;
        }
        public async Task Invoke(HttpContext context)
        {
            //Oluşan log datalarını aldım
            var reqResContext = await base.InvokeBaseMiddleware(context);
        }



    }
}
