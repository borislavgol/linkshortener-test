using AutoMapper;
using LinkShortener.Dal.Entities;
using LinkShortener.Dal.Repositories.Abstractions;
using LinkShortener.Models;
using Microsoft.EntityFrameworkCore;

namespace LinkShortener.Dal.Repositories.Implementations
{
    public class LinksRepository : ILinksRepository
    {
        private readonly IMapper _mapper;
        private readonly DatabaseContext _context;

        public LinksRepository(
            IMapper mapper,
            DatabaseContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<ShortedLinkModel> GetShortedLinkModelAsync(string shortedLink)
        {
            var shortedLinkEntities = await _context.ShortedLinks
                .Where(x => x.ShortLink == shortedLink)
                .FirstOrDefaultAsync();

            if (shortedLinkEntities is null)
            {
                return null;
            }

            return _mapper.Map<ShortedLinkModel>(shortedLinkEntities);
        }

        public async Task<IEnumerable<ShortedLinkModel>> GetShortedLinksRelatedToUserAsync(int userId, int offset, int limit)
        {
            var shortedLinkEntities = await _context.ShortedLinks
                .Where(x => x.OwnerId == userId)
                .Skip(offset)
                .Take(limit)
                .ToListAsync();

            return _mapper.Map<IEnumerable<ShortedLinkModel>>(shortedLinkEntities);
        }

        public async Task<ShortedLinkModel> SaveShortedLinkAsync(int userId, string shortedLink, string originalLink)
        {
            var shortedLinkEntity = (await _context.ShortedLinks.AddAsync(new ShortedLinkEntity
            {
                OwnerId = userId,
                OriginalLink = originalLink,
                ShortLink = shortedLink
            })).Entity;

            await _context.SaveChangesAsync();

            return _mapper.Map<ShortedLinkModel>(shortedLinkEntity);
        }
    }
}
