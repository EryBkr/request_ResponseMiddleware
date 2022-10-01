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
    //Interface bize deploy etmiş olduğumuz nuget paketinden gelmiştir
    //Nuget'tan aldığımız library'imizi local de bir klasöre deploy ederekte alabiliriz, NuGet ayarlarında bu mevcut (örneğin sadece şirket içi kullanılacak bir pakette yapılabilir)
    class FileLoggerMessageCreator : ILogMessageCreator
    {
        public string Create(RequestResponseContext context)
        {
            //DateTime:{0} - [Duration] - [Path+Querystring] - [ReqBody] - [ResBody]
            var stringMessage = $"{DateTime.Now:dd.MM.yyyy HH:mm:ss} - [{context.FormattedCreationTime}] - [{context.Url.PathAndQuery}] - [{context.RequestBody}] - [{context.ResponseBody}]";

            return stringMessage;
        }
    }
}
