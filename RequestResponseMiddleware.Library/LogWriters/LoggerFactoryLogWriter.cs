using Microsoft.Extensions.Logging;
using RequestResponseMiddleware.Library.Interfaces;
using RequestResponseMiddleware.Library.MessageCreators;
using RequestResponseMiddleware.Library.Models;

namespace RequestResponseMiddleware.Library.LogWriters
{
    //Bu Class'ın kendisine , bu proje dışında ulaşmayacağım için internal olarak bıraktım
    internal class LoggerFactoryLogWriter : ILogWriter
    {
        private readonly ILogger _logger;
        private readonly LoggingOptions _options;

        //ILogWriter'dan geliyor
        public ILogMessageCreator MessageCreator { get; }

        public LoggerFactoryLogWriter(ILoggerFactory loggerFactory, LoggingOptions options)
        {
            //Bizim işimiz loggerFactory ile değil aslında logger ile. Bundan kaynaklı bu atamayı burada gerçekleştirdik
            _logger = loggerFactory.CreateLogger(options.LoggerCategoryName);
            _options = options;

            //Message creater'ı oluşturup direkt options'u içerisinde gönderiyorum
            MessageCreator = new LoggerFactoryMessageCreator(options);
        }

       

        public async Task Write(RequestResponseContext context)
        {
            var message = MessageCreator.Create(context);

            //_options.LoggingFields
            _logger.Log(_options.LogLevel, message);

            await Task.CompletedTask;
        }
    }
}
