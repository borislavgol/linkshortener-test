namespace LinkShortener.Services.Abstractions
{
    public interface IBalanceService
    {
        Task<bool> TryChargeMoneyAsync(int userId, decimal chargeValue);
    }
}
