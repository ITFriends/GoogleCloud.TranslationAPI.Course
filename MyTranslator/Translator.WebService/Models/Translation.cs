using System.ComponentModel.DataAnnotations.Schema;

namespace Translator.WebService.Models
{
    public class Translation
    {
        public string Id { get; set; }

        public string Language { get; set; }

        public string TranslatedText { get; set; }

        [ForeignKey("RecordId")]
        public Record Record { get; set; }

        public string RecordId { get; set; }
    }
}
