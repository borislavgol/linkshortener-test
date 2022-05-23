using MediatR;

namespace LinkShortener.Dtos
{
    public class GetShortedLinksRequestDto : IRequest<GetShortedLinksResponseDto>
    {
        public int Offset { get; set; } = 0;

        public int Limit { get; set; } = 10;

        public int UserId { get; set; }
    }
}
