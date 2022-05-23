using LinkShortener.Models;

namespace LinkShortener.Dal.Repositories.Abstractions
{
    public interface IUsersRepository
    {
        Task<UserModel> CreateUserAsync(string login, string password);

        Task<bool> ChargeMoneyByUserIdAsync(int userId, decimal chargeValue);

        Task<UserModel> GetUserByIdAsync(int userId);

        Task<UserModel> GetUserByLoginAsync(string login);

        Task<UserModel> GetUserByLoginPassMatchAsync(string login, string password);
    }
}
