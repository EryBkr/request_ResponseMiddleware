using RequestResponseMiddleware.Library.Models;

namespace RequestResponseMiddleware.Library.Interfaces
{
    public interface ILogWriter
    {
        //Sadece yapıcı metotta set ettiğimiz için setter'ı sildim
        ILogMessageCreator MessageCreator { get;}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        Task Write(RequestResponseContext context);
    }
}
