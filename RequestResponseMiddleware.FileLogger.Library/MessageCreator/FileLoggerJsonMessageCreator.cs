using RequestResponseMiddleware.Library.Interfaces;
using RequestResponseMiddleware.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RequestResponseMiddleware.FileLogger.Library.MessageCreator
{
    //Json Format ta çıktı verebilmek için
    internal class FileLoggerJsonMessageCreator : ILogMessageCreator
    {
        public string Create(RequestResponseContext context)
        {
            return $"{DateTime.Now:dd.MM.yyyy HH:mm:ss} - {JsonSerializer.Serialize(context)}";
        }
    }
}
