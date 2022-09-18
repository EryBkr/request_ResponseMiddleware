using RequestResponseMiddleware.Library.Interfaces;
using RequestResponseMiddleware.Library.Models;

namespace RequestResponseMiddleware.Library.MessageCreators
{
    class LoggerFactoryMessageCreator : BaseLogMessageCreator, ILogMessageCreator
    {
        private readonly LoggingOptions options;

        public LoggerFactoryMessageCreator(LoggingOptions options)
        {
            this.options = options;
        }


        public string Create(RequestResponseContext context)
        {
            var sb = new StringBuilder();

            //Create return String
            foreach (var field in options.LoggingFields)
            {
                //Benden istenen options'a göre gerekli değerleri alıyorum
                var value = base.GetValueByField(context, field);

                //PropertyName: value -- gibi bir değer aslında
                sb.AppendFormat("{0}: {1}\n", field, value);
            }


            return sb.ToString();
        }


    }
}
