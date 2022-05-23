using LinkShortener.Models;

namespace LinkShortener.Dal.Repositories.Abstractions
{
    public interface ILinksRepository
    {
        Task<IEnumerable<ShortedLinkModel>> GetShortedLinksRelatedToUserAsync(int userId, int offset, int limit);

        Task<ShortedLinkModel> GetShortedLinkModelAsync(string shortedLink);

        Task<ShortedLinkModel> SaveShortedLinkAsync(int userId, string shortedLink, string originalLink);
    }
}
