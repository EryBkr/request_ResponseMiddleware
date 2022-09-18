using RequestResponseMiddleware.Library.Models;

namespace RequestResponseMiddleware.Library.Interfaces
{
    public interface ILogWriter
    {

        Task Write(RequestResponseContext context);
    }
}
