using System;
using System.IO;

namespace RequestResponseMiddleware.FileLogger.Library.Models
{
    //Loglama ile ilgili ayarları wrap'leyecek modelimiz
    public class FileLoggingOptions
    {
        //Klasör bilgisi
        public string FileDirectory { get; set; }

        public string FileName { get; set; } = "logs";
        public string Extension { get; set; } = "txt";

        //File mevcut değilse oluştur
        public bool ForceCreateDirectory { get; set; } = true;

        //Loglama tipi json mı olsun
        public bool UseJsonFormat { get; set; } = false;


        internal string GetFullFileName() => $"{FileName}.{Extension}";
        internal string GetFullFilePath() => Path.Combine(FileDirectory, GetFullFileName());


        internal void ValidatePath()
        {
            try
            { 
                //Klasörü create etmeye çalışıyoruz, bununla ilgili bir hata yakalıyorsak ona göre dönüş yapacağız (tabi otomatik olarak oluşturulması bekleniyorsa deneme yapılacak)
                if (ForceCreateDirectory)
                    Directory.CreateDirectory(FileDirectory);
                else
                {
                    if (Directory.Exists(FileDirectory))
                        throw new Exception($"{FileDirectory} not found");
                }
            }
            catch (UnauthorizedAccessException exception)
            {

                throw;
            }
        }
    }
}
