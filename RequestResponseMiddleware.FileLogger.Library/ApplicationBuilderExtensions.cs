using Microsoft.AspNetCore.Builder;
using RequestResponseMiddleware.FileLogger.Library.LogWriters;
using RequestResponseMiddleware.FileLogger.Library.Models;
using RequestResponseMiddleware.Library.Interfaces;
using RequestResponseMiddleware.Library.Middlewares;
using System;

namespace RequestResponseMiddleware.FileLogger.Library
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder AddRequestResponseFileLoggerMiddleware(this IApplicationBuilder appBuilder, Action<FileLoggingOptions> optionAction)
        {
            //Bana new operasyonu yapılmadan gelen option değerlerimin instance'ını burada oluşturuyorum.Kullanıcının bu operasyonu yapması sağlıklı değil
            var opt = new FileLoggingOptions();
            optionAction(opt);

            //Kullanıcı tarafından gelen options'u FileLogWritter class'ıma veriyorum
            ILogWriter _logWriter = new FileLogWriter(opt);

            appBuilder.UseMiddleware<LoggingMiddleware>(_logWriter);

            return appBuilder;
        }
    }
}
