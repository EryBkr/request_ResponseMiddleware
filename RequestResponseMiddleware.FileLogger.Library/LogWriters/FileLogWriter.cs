using RequestResponseMiddleware.FileLogger.Library.MessageCreator;
using RequestResponseMiddleware.FileLogger.Library.Models;
using RequestResponseMiddleware.Library.Interfaces;
using RequestResponseMiddleware.Library.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestResponseMiddleware.FileLogger.Library.LogWriters
{
    class FileLogWriter : ILogWriter
    {
        //Kullanıcının log seçeneklerini belirleyebildiği property
        private readonly FileLoggingOptions _fileLoggingOptions;

        public ILogMessageCreator MessageCreator { get; }

        //Yapıcı bir defa çağrılacak ve bundan dolayı gerekli instance oluşturma ve ayarlar burada bir kez tanımlanacak şekilde implemente edildi
        public FileLogWriter(FileLoggingOptions fileLoggingOptions)
        {
            _fileLoggingOptions = fileLoggingOptions;

            //Options'a bağlı olarak message creator'ı oluşturuyoruz
            MessageCreator = fileLoggingOptions.UseJsonFormat
                ? new FileLoggerJsonMessageCreator()
                : new FileLoggerJsonMessageCreator();

            //Validasyon da bir kez check edilecek o yüzden burada çağırdık
            _fileLoggingOptions.ValidatePath();
        }



        public async Task Write(RequestResponseContext context)
        {
            //Options'a göre oluşturduğumuz Create metodu burada çağrılıyor ve log mesajı alınıyor
            var message = MessageCreator.Create(context);

            var fullPath = _fileLoggingOptions.GetFullFilePath();

            //Loglama işlemi dosyaya yapılacak
            await File.AppendAllTextAsync(fullPath, message);
        }
    }
}
