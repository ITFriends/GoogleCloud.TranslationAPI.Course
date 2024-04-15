using Google.Api.Gax.ResourceNames;
using Google.Cloud.Translate.V3;

namespace MyTranslator.Advanced
{
    internal class Program
    {
        private static readonly string projectId = "<put_your_google_cloud_project_id_here>";

        static void Main(string[] args)
        {
            Console.WriteLine("Добро пожаловать в простейший переводчик");

            var client = TranslationServiceClient.Create();

            Console.WriteLine("Введите текст для перевода: ");
            var textToTranslate = Console.ReadLine();

            var request = new TranslateTextRequest
            {
                Contents = { textToTranslate },
                TargetLanguageCode = "en-US",
                Parent = new ProjectName(projectId).ToString()
            };

            TranslateTextResponse response = client.TranslateText(request);

            // response.Translations will have one entry, because request.Contents has one entry.
            Translation translation = response.Translations[0];
            Console.WriteLine($"\r\nОпределенный язык: {translation.DetectedLanguageCode}");
            Console.WriteLine($"Результат: {translation.TranslatedText}");

            Console.ReadKey();
        }
    }
}