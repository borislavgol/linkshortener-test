using LinkShortener.Dal.Repositories.Abstractions;
using LinkShortener.Models;
using LinkShortener.Services.Abstractions;

namespace LinkShortener.Services.Implementations
{
    public class LinkShortenService : ILinkShortenService
    {
        private readonly ILinksRepository _linksRepository;

        public LinkShortenService(
            ILinksRepository linksRepository)
        {
            _linksRepository = linksRepository;
        }

        public async Task<string> ShortenAndSaveLinkAsync(ShortenLinkModel shortenLinkModel)
        {
            string newLink = ToShortString(Guid.NewGuid());

            var shortedLinkModel = await _linksRepository.SaveShortedLinkAsync(shortenLinkModel.UserId, newLink, shortenLinkModel.Link);

            return shortedLinkModel.ShortedLink;
        }

        public Task<IEnumerable<ShortedLinkModel>> GetShortedLinksRelatedToUserAsync(int userId, int offset, int limit)
        {
            return _linksRepository.GetShortedLinksRelatedToUserAsync(userId, offset, limit);
        }

        public Task<ShortedLinkModel> GetShortedLinkAsync(string shortedLink)
        {
            return _linksRepository.GetShortedLinkModelAsync(shortedLink);
        }

        private string ToShortString(Guid guid)
        {
            var base64Guid = Convert.ToBase64String(guid.ToByteArray());

            // Replace URL unfriendly characters
            base64Guid = base64Guid.Replace('+', '-').Replace('/', '_');

            // Remove the trailing ==
            return base64Guid.Substring(0, base64Guid.Length - 2);
        }
    }
}
