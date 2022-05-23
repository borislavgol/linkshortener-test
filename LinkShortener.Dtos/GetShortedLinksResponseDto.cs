namespace LinkShortener.Dtos
{
    public class GetShortedLinksResponseDto
    {
        public IEnumerable<ShortedLinkDto> Links { get; set; }
    }
}
