using RequestResponseMiddleware.Library.Interfaces;
using RequestResponseMiddleware.Library.Models;
using System.Text.Json;

namespace RequestResponseMiddlewareFileLogger.Library.MessageCreator
{
    //Json olarak istenirse
    internal class FileLoggerJsonMessageCreator : ILogMessageCreator
    {
        public string Create(RequestResponseContext context)
        {
            return $"{DateTime.Now:dd.MM.yyyy HH:mm:ss} - {JsonSerializer.Serialize(context)}";
        }
    }
}
