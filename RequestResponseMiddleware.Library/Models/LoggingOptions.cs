using Microsoft.Extensions.Logging;

namespace RequestResponseMiddleware.Library.Models
{
    //Log seçimi için ayarları wrap'leyeceğimiz model
    public class LoggingOptions
    {
        private List<LogFields> _loggingFields;

        public LogLevel LogLevel { get; set; } = LogLevel.Information; //Default value was setted
        public string LoggerCategoryName { get; set; } = "RequestResponseLoggerMiddleware";

        //Hangi alanları talep edeceğiz
        public List<LogFields> LoggingFields
        {
            get
            {
                //Bu arada aslında gereksiz new operasyonun önüne geçiyoruz, erişilip değer atanacaksa new işlemini gerçekleştiriyoruz
                return _loggingFields ??= new List<LogFields>();
            }
            set => _loggingFields = value;
        }
    }

    //Hangi field ların gerekeceği ile ilgili kararı aktaracağımız enum
    public enum LogFields
    {
        Request,
        Response,
        HostName,
        Path,
        QueryString,
        ResponseTiming,
        RequestLength,
        ResponseLength
    }
}
