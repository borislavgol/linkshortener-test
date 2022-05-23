using LinkShortener.Dtos;
using LinkShortener.Services.Abstractions;
using MediatR;

namespace LinkShortener.Mediatr.Handlers
{
    public class ChargeMoneyHandler : IRequestHandler<ChargeMoneyRequestDto, ChargeMoneyResponseDto>
    {
        private readonly IBalanceService _balanceService;

        public ChargeMoneyHandler(
            IBalanceService balanceService)
        {
            _balanceService = balanceService;
        }

        public async Task<ChargeMoneyResponseDto> Handle(ChargeMoneyRequestDto request, CancellationToken cancellationToken)
        {
            var result = await _balanceService.TryChargeMoneyAsync(request.UserId, request.ChargeValue);

            return new ChargeMoneyResponseDto
            {
                IsSuccess = result
            };
        }
    }
}
