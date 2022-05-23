using MediatR;

namespace LinkShortener.Dtos
{
    public class ChargeMoneyRequestDto : IRequest<ChargeMoneyResponseDto>
    {
        public int UserId { get; set; }

        public decimal ChargeValue { get; set; }
    }
}
