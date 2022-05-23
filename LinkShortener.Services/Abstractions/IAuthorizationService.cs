using LinkShortener.Models;

namespace LinkShortener.Services.Abstractions
{
    public interface IAuthorizationService
    {
        Task<UserModel> AuthorizeUserAsync(string login, string password);
        Task<UserModel> CreateUserAsync(string login, string password);
    }
}
