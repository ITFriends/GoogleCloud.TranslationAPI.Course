using Google.Api.Gax.ResourceNames;
using Google.Cloud.Translate.V3;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Translator.WebService.Data;
using Translator.WebService.Models;

namespace Translator.WebService.BackgroundServices
{
    public class TranslationService : BackgroundService
    {
        private readonly ILogger<TranslationService> _logger;

        private readonly IServiceScopeFactory _scopeFactory;

        private const string projectId = "<put_your_google_cloud_project_id_here>";

        public TranslationService(ILogger<TranslationService> logger, IServiceScopeFactory scopeFactory)
        {
            _logger = logger;
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using PeriodicTimer timer = new(TimeSpan.FromSeconds(5));

            while (!stoppingToken.IsCancellationRequested && await timer.WaitForNextTickAsync(stoppingToken))
            {
                _logger.LogInformation("Translation service started");

                using var scope = _scopeFactory.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                var recordsForTranslation = await db.Records.Include(r => r.Translations)
                                                      .Where(r => !r.Translations.Any())
                                                      .ToListAsync(CancellationToken.None);

                foreach (var record in recordsForTranslation)
                {
                    await DoTranslation(db, record);
                }
            }
        }

        private static async Task DoTranslation(ApplicationDbContext db, Record record)
        {
            var client = TranslationServiceClient.Create();

            var request = new TranslateTextRequest
            {
                Contents = { record.Text },
                TargetLanguageCode = "en-US",
                Parent = new ProjectName(projectId).ToString()
            };

            TranslateTextResponse response = client.TranslateText(request);

            var translation = new Models.Translation
            {
                Id = Guid.NewGuid().ToString(),
                Language = "en-US",
                TranslatedText = response.Translations[0].TranslatedText,
                RecordId = record.Id
            };

            await db.Translations.AddAsync(translation);
            await db.SaveChangesAsync();
        }
    }
}
