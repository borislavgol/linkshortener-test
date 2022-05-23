using AutoMapper;
using LinkShortener.Dtos;
using LinkShortener.Services.Abstractions;
using MediatR;

namespace LinkShortener.Mediatr.Handlers
{
    public class GetShortedLinksHandler : IRequestHandler<GetShortedLinksRequestDto, GetShortedLinksResponseDto>
    {
        private readonly IMapper _mapper;
        private readonly ILinkShortenService _linkShortenService;

        public GetShortedLinksHandler(
            IMapper mapper,
            ILinkShortenService linkShortenService)
        {
            _mapper = mapper;
            _linkShortenService = linkShortenService;
        }

        public async Task<GetShortedLinksResponseDto> Handle(GetShortedLinksRequestDto request, CancellationToken cancellationToken)
        {
            var links = await _linkShortenService.GetShortedLinksRelatedToUserAsync(request.UserId, request.Offset, request.Limit);

            return new GetShortedLinksResponseDto
            {
                Links = _mapper.Map<IEnumerable<ShortedLinkDto>>(links)
            };
        }
    }
}
