using Microsoft.AspNetCore.Http;
using Microsoft.IO;
using RequestResponseMiddleware.Library.Interfaces;
using RequestResponseMiddleware.Library.Models;
using System.Diagnostics;
using System.IO;

namespace RequestResponseMiddleware.Library.Middlewares
{
    /// <summary>
    /// Her iki middleware'de kullanılan yapıyı ortak bir yere aldık
    /// Abstract tanımlamamızın sebebi, başka bir yerde instance'ı alınmasın
    /// </summary>
    public abstract class BaseRequestResponseMiddleware
    {
        private readonly RequestResponseOptions _options;
        private readonly RecyclableMemoryStreamManager recyclableMemoryStreamManager;
        private readonly RequestDelegate _next;
        private readonly ILogWriter logWriter;

        public BaseRequestResponseMiddleware(RequestDelegate next, ILogWriter _logWriter)
        {
            _next = next;
            logWriter = _logWriter;
        }

        //Sadece inherit edildiği yerde kullanılabilsin
        protected async Task<RequestResponseContext> InvokeBaseMiddleware(HttpContext context)
        {
            var requestBody = await GetRequestBody(context);

            //Response Handling
            var originalBodyStream = context.Response.Body;
            await using var responseBody = recyclableMemoryStreamManager.GetStream();
            //ResponseBody'nin orjinal halini saklamak için atama işlemi yapıyoruz
            context.Response.Body = responseBody;

            //Timer için Request öncesinde başlatıyorum
            var sw = Stopwatch.StartNew();

            //Request
            await _next(context); //Executing
            //Response

            //Response sonrası Timer'ı kapatıyorum
            sw.Stop();


            context.Request.Body.Seek(0, SeekOrigin.Begin);

            //Context'teki response body'nin başından sonuna kadar okuyorum
            string responseBodyText = await new StreamReader(context.Response.Body).ReadToEndAsync();

            //Tek başa alıyorum diğer middleware'lerin okuyabilmesi için
            context.Request.Body.Seek(0, SeekOrigin.Begin);


            //HTTPContext'ten beslenerek, gerekli log modelimizi oluşturuyoruz
            var result = new RequestResponseContext(context)
            {
                ResponseCreationTime = TimeSpan.FromTicks(sw.ElapsedTicks),
                RequestBody = requestBody,
                ResponseBody = responseBodyText
            };

            //Gelen modelin loglanması için log servisime gönderiyorum
            await logWriter.Write(result);

            return result;
        }

        private static string ReadStreamInChunks(Stream stream)
        {
            const int readChunkBufferLength = 4096;

            stream.Seek(0, SeekOrigin.Begin);

            using var textWriter = new StringWriter();
            using var reader = new StreamReader(stream, Encoding.UTF8);

            var readChunk = new char[readChunkBufferLength];
            int readChunkLength;

            do
            {
                readChunkLength = reader.ReadBlock(readChunk,
                                                   0,
                                                   readChunkBufferLength);
                textWriter.Write(readChunk, 0, readChunkLength);

            } while (readChunkLength > 0);

            return textWriter.ToString();
        }

        private async Task<string> GetRequestBody(HttpContext context)
        {
            context.Request.EnableBuffering();

            //recyclableMemoryStreamManager memory'i daha efektif kullanabilmek için ekledik
            await using var requestStream = recyclableMemoryStreamManager.GetStream();
            await context.Request.Body.CopyToAsync(requestStream);

            string reqBody = ReadStreamInChunks(requestStream);

            context.Request.Body.Seek(0, SeekOrigin.Begin);

            return reqBody;
        }
    }
}
