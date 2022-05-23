using LinkShortener.Models;

namespace LinkShortener.Services.Abstractions
{
    public interface ILinkShortenService
    {
        Task<string> ShortenAndSaveLinkAsync(ShortenLinkModel shortenLinkModel);
        
        Task<IEnumerable<ShortedLinkModel>> GetShortedLinksRelatedToUserAsync(int userId, int offset, int limit);

        Task<ShortedLinkModel> GetShortedLinkAsync(string shortedLink);
    }
}
