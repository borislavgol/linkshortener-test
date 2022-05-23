using LinkShortener.Dal.Repositories.Abstractions;
using LinkShortener.Models;
using LinkShortener.Services.Abstractions;

namespace LinkShortener.Services.Implementations
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly IUsersRepository _usersRepository;

        public AuthorizationService(
            IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }
        public async Task<UserModel> CreateUserAsync(string login, string password)
        {
            if (await _usersRepository.GetUserByLoginAsync(login) is not null)
            {
                return null;
            }

            return await _usersRepository.CreateUserAsync(login, password);
        }

        public async Task<UserModel> AuthorizeUserAsync(string login, string password)
        {
            return await _usersRepository.GetUserByLoginPassMatchAsync(login, password);
        }
    }
}
