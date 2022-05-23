using MediatR;

namespace LinkShortener.Dtos
{
    public class GetOriginalLinkRequestDto : IRequest<GetOriginalLinkResponseDto>
    {
        public string ShortedLink { get; set; }
    }
}
