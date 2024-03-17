using Google.Cloud.Translation.V2;

namespace MyTranslator.Basic
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите текст для перевода: ");
            var textToTranslate = Console.ReadLine();

            var client = TranslationClient.Create();
            var result = client.TranslateText(textToTranslate, LanguageCodes.English);

            Console.WriteLine($"\r\nРезультат: {result.TranslatedText}; detected language {result.DetectedSourceLanguage}\r\n");
            
            Console.ReadKey();
        }
    }
}