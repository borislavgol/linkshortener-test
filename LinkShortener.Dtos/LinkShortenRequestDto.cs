using MediatR;
using System.ComponentModel.DataAnnotations;

namespace LinkShortener.Dtos
{
    public class LinkShortenRequestDto : IRequest<LinkShortenResultDto>
    {
        public int UserId { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Link { get; set; }
    }
}
