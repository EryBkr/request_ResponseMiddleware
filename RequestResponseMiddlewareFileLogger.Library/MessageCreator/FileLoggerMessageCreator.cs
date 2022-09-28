using RequestResponseMiddleware.Library.Interfaces;
using RequestResponseMiddleware.Library.Models;


namespace RequestResponseMiddlewareFileLogger.Library.MessageCreator
{
    public class FileLoggerMessageCreator : ILogMessageCreator
    {
        public string Create(RequestResponseContext context)
        {
            //DateTime: {0} - [Duration] - [Path+Querystring] - [ReqBody] - [ResBody]
            string message = $"{DateTime.Now:dd.MM.yyyy HH:mm:ss} - [{context.FormattedCreationTime}] - [{context.Url.PathAndQuery}] - [{context.RequestBody}] - [{context.ResponseBody}]";


            return message;
        }
    }
}
