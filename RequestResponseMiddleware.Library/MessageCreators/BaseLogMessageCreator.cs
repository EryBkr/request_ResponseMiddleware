using RequestResponseMiddleware.Library.Models;

namespace RequestResponseMiddleware.Library.MessageCreators
{
    //instance alınmasın, sadece inherit olması için abstract olarak tanımladım
    public abstract class BaseLogMessageCreator
    {
        //Seçilen Log Field lara uygun olan yapıyı kuruyoruz
        //Bir kaç farklı log mekanizmasında kullanabileceğimizden dolayı base class'a tanımladık
        protected string GetValueByField(RequestResponseContext _context, LogFields field)
        {
            return field switch
            {
                LogFields.Request => _context.RequestBody,
                LogFields.Response => _context.ResponseBody,
                LogFields.QueryString => _context.context?.Request?.QueryString.Value,
                LogFields.Path => _context.context?.Request?.Path,
                LogFields.HostName => _context.context?.Request?.Host.Value,
                LogFields.RequestLength => _context.RequestLength.ToString(),
                LogFields.ResponseLength => _context.ResponseLength.ToString(),
                LogFields.ResponseTiming => _context.FormattedCreationTime,
                _ => string.Empty //Default alt-tire ile tanımlanıyor
            };
        }
    }
}
