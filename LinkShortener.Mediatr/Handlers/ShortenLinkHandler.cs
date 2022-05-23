using AutoMapper;
using LinkShortener.Dtos;
using LinkShortener.Exceptions.Balance;
using LinkShortener.Models;
using LinkShortener.Services.Abstractions;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace LinkShortener.Mediatr.Handlers
{
    public class ShortenLinkHandler : IRequestHandler<LinkShortenRequestDto, LinkShortenResultDto>
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly ILinkShortenService _linkShortenService;
        private readonly IConfiguration _configuration;

        public ShortenLinkHandler(
            IMapper mapper,
            IMediator mediator,
            ILinkShortenService linkShortenService,
            IConfiguration configuration)
        {
            _mapper = mapper;
            _mediator = mediator;
            _linkShortenService = linkShortenService;
            _configuration = configuration;
        }

        public async Task<LinkShortenResultDto> Handle(LinkShortenRequestDto request, CancellationToken cancellationToken)
        {
            var shortenLinkModel = _mapper.Map<ShortenLinkModel>(request);

            var shorteningPrice = decimal.Parse(_configuration.GetSection("ShorteningPrice").Value);

            var chargeResult = await _mediator.Send(new ChargeMoneyRequestDto { UserId = request.UserId, ChargeValue = shorteningPrice }, cancellationToken);

            if (chargeResult.IsSuccess)
            {
                var shortedLink = await _linkShortenService.ShortenAndSaveLinkAsync(shortenLinkModel);

                return new LinkShortenResultDto
                {
                    ResultLink = shortedLink
                };
            }

            throw new NotEnoughBalanceException("Not enough balance");
        }
    }
}
