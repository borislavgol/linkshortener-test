using LinkShortener.Dtos;
using LinkShortener.Exceptions.Balance;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LinkShortener.Web.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize(AuthenticationSchemes = "Cookies")]
    public class LinkShortenerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LinkShortenerController(
            IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get links shorted by authenticated user
        /// </summary>
        [HttpPost("links")]
        public async Task<ActionResult<GetShortedLinksResponseDto>> GetAsync(int offset = 0, int limit = 10, CancellationToken cancellationToken = default)
        {
            var getShortedLinksRequestDto = new GetShortedLinksRequestDto
            {
                Offset = offset,
                Limit = limit,
                UserId = int.Parse(User.Claims.First(x => x.Type == "UserId").Value)
            };

            return await _mediator.Send(getShortedLinksRequestDto, cancellationToken);
        }

        /// <summary>
        /// Shorten the link
        /// </summary>
        [HttpPost("shorten")]
        public async Task<ActionResult<LinkShortenResultDto>> ShortenAsync([FromBody] LinkShortenRequestDto linkShortenRequestDto, CancellationToken cancellationToken)
        {
            linkShortenRequestDto.UserId = int.Parse(User.Claims.First(x => x.Type == "UserId").Value);

            try
            {
                return await _mediator.Send(linkShortenRequestDto, cancellationToken);
            }
            catch (NotEnoughBalanceException exception)
            {
                return BadRequest(exception.Message);
            }
        }
    }
}
