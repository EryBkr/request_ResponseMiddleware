using Microsoft.AspNetCore.Builder;
using RequestResponseMiddleware.Library.Interfaces;
using RequestResponseMiddleware.Library.LogWriters;
using RequestResponseMiddleware.Library.Middlewares;
using RequestResponseMiddleware.Library.Models;

namespace RequestResponseMiddleware.Library
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder AddRequestResponseMiddleware(this IApplicationBuilder appBuilder, Action<RequestResponseOptions> optionAction)
        {
            //Bana new operasyonu yapılmadan gelen option değerlerimin instance'ını burada oluşturuyorum.Kullanıcının bu operasyonu yapması sağlıklı değil
            var opt = new RequestResponseOptions();
            optionAction(opt);


            //Log Factory'inin gelip gelmemesi senaryosuna göre oluşturdum
            ILogWriter _logWriter = opt._loggerFactory is null 
                ? new NullLogWriter()
                : new LoggerFactoryLogWriter(opt._loggerFactory, opt._loggingOptions);


            //Burada hangi tarz middleware ekleyeceğimin kararını veriyorum
            if (opt.ReqResHandler is not null)
                appBuilder.UseMiddleware<HandlerRequestResponseLoggingMiddleware>(opt.ReqResHandler, _logWriter);
            else
                appBuilder.UseMiddleware<LoggingMiddleware>(opt.ReqResHandler, _logWriter);

            return appBuilder;
        }
    }
}
