using Microsoft.AspNetCore.Builder;
using RequestResponseMiddleware.Library.Interfaces;
using RequestResponseMiddleware.Library.Middlewares;
using RequestResponseMiddlewareFileLogger.Library.LogWriters;
using RequestResponseMiddlewareFileLogger.Library.Models;

namespace RequestResponseMiddlewareFileLogger.Library
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder AddRequestResponseFileLoggerMiddleware(this IApplicationBuilder appBuilder, Action<FileLoggingOptions> optionAction)
        {
            //Bana new operasyonu yapılmadan gelen option değerlerimin instance'ını burada oluşturuyorum.Kullanıcının bu operasyonu yapması sağlıklı değil
            var opt = new FileLoggingOptions();
            optionAction(opt);


            //ILogWriter'ın instance'ını oluşturuyorum,json tipine göre ayrımı zaten kendi içerisinde yapıyor
            ILogWriter _logWriter = new FileLogWriter(opt);


            appBuilder.UseMiddleware<LoggingMiddleware>(_logWriter);


            return appBuilder;
        }
    }
}
