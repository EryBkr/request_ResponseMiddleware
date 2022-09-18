using RequestResponseMiddleware.Library.Models;

namespace RequestResponseMiddleware.Library.Interfaces
{
    /// <summary>
    /// Log'un farklı formatlarda yazılması gerekebileceğinden dolayı oluşturduk
    /// </summary>
    public interface ILogMessageCreator
    {
        string Create(RequestResponseContext context);
    }
}
