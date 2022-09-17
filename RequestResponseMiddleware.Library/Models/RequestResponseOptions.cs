using Microsoft.Extensions.Logging;

namespace RequestResponseMiddleware.Library.Models
{
    public class RequestResponseOptions
    {
        //Func Delegate
        //Func<Parameter Type,return Type>
        internal Func<RequestResponseContext, Task> ReqResHandler { get; set; }

        internal LoggingOptions _loggingOptions;
        internal ILoggerFactory _loggerFactory;


        /// <summary>
        /// Log sonuçlarını handle etmek için kullanılır,kendisi ne yaparsa yapar
        /// </summary>
        /// <param name="reqResHandler"></param>
        //Func delegemi doldurmak için fonksiyon oluşturdum, setter gibi aslında
        public void UseHandler(Func<RequestResponseContext, Task> reqResHandler) => ReqResHandler = reqResHandler;


        /// <summary>
        /// Direkt log ları basmak amacıyla kullanılır
        /// </summary>
        /// <param name="loggerFactory"></param>
        /// <param name="loggingActions"></param>
        //Action kullanma sebebimiz kullanıcı fonksiyonumuzu kullanırken new operasyonu yapmasın,biz bu işi halledelim
        public void UseLogger(ILoggerFactory loggerFactory, Action<LoggingOptions> loggingActions)
        {
            //New operasyonunu kullanıcı yerine biz kendimiz,kütüphanemiz içerisinde yaptık
            _loggingOptions = new LoggingOptions();

            //Burada da atama işlemini gerçekleştirdik
            loggingActions(_loggingOptions);

            //Kullanıcı kendi factory yapısını verdi
            _loggerFactory = loggerFactory;
        }
    }
}
