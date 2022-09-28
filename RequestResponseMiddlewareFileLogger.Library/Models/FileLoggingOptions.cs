using System.IO;

namespace RequestResponseMiddlewareFileLogger.Library.Models
{
    //Dosya ayarları
    public class FileLoggingOptions
    {
        public string FileDirectory { get; set; } //Dosya yolu
        public string FileName { get; set; } = "logs"; //Dosya adı
        public string Extension { get; set; } = "txt"; //Dosya uzantısı
        public bool ForceCreateDirectory { get; set; } = true; //Dosya oluşturulsun mu?
        public bool UseJsonFormat { get; set; } = false; //Json formatta mı loglayalım

        //Dosya ismini uzantısıyla birlikte al
        internal string GetFullFileName() => $"{FileName}.{Extension}";

        //Path'in tamamını al
        internal string GetFullFilePath() => Path.Combine(FileDirectory, GetFullFileName());

        //Dosya path'ini sorgula
        internal void ValidatePath()
        {
            try
            {
                if (ForceCreateDirectory)
                    Directory.CreateDirectory(FileDirectory);
                else
                {
                    if (!Directory.Exists(FileDirectory))
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
