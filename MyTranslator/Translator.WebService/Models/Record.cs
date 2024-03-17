namespace Translator.WebService.Models
{
    public class Record
    {
        public string Id { get; set; }

        public string Text { get; set; }

        public ICollection<Translation> Translations { get; set; }
    }
}
