namespace LinkShortener.Models
{
    public class ShortedLinkModel
    {
        public int Id { get; set; }

        public int OwnerId { get; set; }

        public string OriginalLink { get; set; }

        public string ShortedLink { get; set; }
    }
}
