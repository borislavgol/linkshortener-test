using LinkShortener.Dal.Repositories.Abstractions;
using LinkShortener.Services.Abstractions;

namespace LinkShortener.Services.Implementations
{
    public class BalanceService : IBalanceService
    {
        private readonly IUsersRepository _usersRepository;

        public BalanceService(
            IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public async Task<bool> TryChargeMoneyAsync(int userId, decimal chargeValue)
        {
            return await _usersRepository.ChargeMoneyByUserIdAsync(userId, chargeValue);
        }
    }
}
