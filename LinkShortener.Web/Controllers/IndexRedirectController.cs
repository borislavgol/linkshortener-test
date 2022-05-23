using LinkShortener.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace LinkShortener.Web.Controllers
{
    [ApiController]
    [Route("link")]
    public class IndexRedirectController : ControllerBase
    {
        private readonly IMediator _mediatr;

        public IndexRedirectController(
            IMediator mediatr)
        {
            _mediatr = mediatr;
        }

        [HttpGet("{shortedLink}")]
        public async Task<IActionResult> Redirect(string shortedLink, CancellationToken cancellationToken)
        {
            var originalLink = await _mediatr.Send(new GetOriginalLinkRequestDto
            {
                ShortedLink = shortedLink
            }, cancellationToken);

            if (originalLink.OriginalLink is null)
            {
                return NotFound();
            }

            return Redirect(originalLink.OriginalLink);
        }
    }
}
