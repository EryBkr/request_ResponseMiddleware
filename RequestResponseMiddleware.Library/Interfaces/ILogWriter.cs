using RequestResponseMiddleware.Library.Models;

namespace RequestResponseMiddleware.Library.Interfaces
{
    public interface ILogWriter
    {

        ILogMessageCreator MessageCreator { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        Task Write(RequestResponseContext context);
    }
}
