using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using System.Text.Json.Serialization;

namespace RequestResponseMiddleware.Library.Models
{
    //Model
    public class RequestResponseContext
    {
        private readonly HttpContext context;

        public RequestResponseContext(HttpContext context)
        {
            this.context = context;
        }

        public string RequestBody { get; set; }
        public string ResponseBody { get; set; }


        //Request Body boş olabilir,ondan kaynaklı nullable tanımladık
        public int? RequestLength => RequestBody?.Length;

        //Response Body boş olabilir,ondan kaynaklı nullable tanımladık
        public int? ResponseLength => ResponseBody?.Length;

        //Response süresi
        [JsonIgnore] //TimeSpan içerisinde bir çok proprty barındıyoruz,o yüzden formatlanmış olarak geri dönmeyi ve bu field'ı ignore etmeyi uygun gördük
        public TimeSpan ResponseCreationTime { get; set; }

        //Formatlanmış halde TimeSpan verimizi dönüyoruz
        public string FormattedCreationTime =>
            FormattedCreationTime is null
            ? "00:00:000"
            : string.Format("{0:mm\\:ss\\.fff}", ResponseCreationTime);

        public Uri Url => BuildUrl();

        //Get Request URL
        internal Uri BuildUrl()
        {
            //GetDisplayUrl Extension metoduna erişebilmek için Microsoft.AspNetCore.Http.Extensions; paketi yüklenmiştir
            var url = context.Request.GetDisplayUrl();

            //İstek adresini dönüyoruz
            return new Uri(url, UriKind.RelativeOrAbsolute);
        }
    }
}
