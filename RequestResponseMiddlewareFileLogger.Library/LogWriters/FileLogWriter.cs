using RequestResponseMiddleware.Library.Interfaces;
using RequestResponseMiddleware.Library.Models;
using RequestResponseMiddlewareFileLogger.Library.MessageCreator;
using RequestResponseMiddlewareFileLogger.Library.Models;
using System.IO;

namespace RequestResponseMiddlewareFileLogger.Library.LogWriters
{
    internal class FileLogWriter : ILogWriter
    {
        public ILogMessageCreator MessageCreator { get; }

        //Dosya ayarlarını dışarıdan alıyoruz (Dışarı dediğimiz uygulama aslında)
        private readonly FileLoggingOptions _options;

        internal FileLogWriter(FileLoggingOptions options)
        {
            _options = options;

            //Options'ta belirtilen seçeneğe göre çıktımızı oluşturuyoruz
            MessageCreator = options.UseJsonFormat
                ? new FileLoggerJsonMessageCreator()
                : new FileLoggerMessageCreator();

            //Seçenekleri validation testinden geçiriyoruz
            _options.ValidatePath();
        }

      
        public async Task Write(RequestResponseContext context)
        {
            //Mesajı oluşturuyoruz
            var message = MessageCreator.Create(context);

            var fullPath = _options.GetFullFilePath();


            await File.AppendAllTextAsync(fullPath, message);
        }
    }
}
