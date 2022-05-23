using LinkShortener.Dtos;
using LinkShortener.Services.Abstractions;
using MediatR;
using StackExchange.Redis.Extensions.Core.Abstractions;

namespace LinkShortener.Mediatr.Handlers
{
    internal class GetOriginalLinkHandler : IRequestHandler<GetOriginalLinkRequestDto, GetOriginalLinkResponseDto>
    {
        private readonly ILinkShortenService _linkShortenService;
        private readonly IRedisClient _redisClient;

        public GetOriginalLinkHandler(
            ILinkShortenService linkShortenService,
            IRedisClient redisClient)
        {
            _linkShortenService = linkShortenService;
            _redisClient = redisClient;
        }

        public async Task<GetOriginalLinkResponseDto> Handle(GetOriginalLinkRequestDto request, CancellationToken cancellationToken)
        {
            var originalLink = await _redisClient.Db0.GetAsync<string>(request.ShortedLink);

            if (originalLink is not null)
            {
                return new GetOriginalLinkResponseDto
                {
                    OriginalLink = originalLink
                };
            }

            var link = await _linkShortenService.GetShortedLinkAsync(request.ShortedLink);

            if (link is null)
            {
                return new GetOriginalLinkResponseDto
                {
                    OriginalLink = null
                };
            }

            await _redisClient.Db0.AddAsync(link.ShortedLink, link.OriginalLink, expiresIn: TimeSpan.FromMinutes(30));

            return new GetOriginalLinkResponseDto
            {
                OriginalLink = link?.OriginalLink
            };
        }
    }
}
