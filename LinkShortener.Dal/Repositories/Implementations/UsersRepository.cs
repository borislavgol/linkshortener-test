using AutoMapper;
using LinkShortener.Dal.Entities;
using LinkShortener.Dal.Repositories.Abstractions;
using LinkShortener.Models;
using Microsoft.EntityFrameworkCore;

namespace LinkShortener.Dal.Repositories.Implementations
{
    public class UsersRepository : IUsersRepository
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;

        public UsersRepository(
            IMapper mapper,
            DatabaseContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<UserModel> CreateUserAsync(string login, string password)
        {
            var newUserEntity = new UserEntity
            {
                Login = login,
                Password = password,
                Balance = new BalanceEntity
                {
                    Balance = 0
                }
            };

            var userEntity = (await _context.Users.AddAsync(newUserEntity)).Entity;

            await _context.SaveChangesAsync();

            return _mapper.Map<UserModel>(userEntity);
        }

        public async Task<bool> ChargeMoneyByUserIdAsync(int userId, decimal chargeValue)
        {
            var userEntity = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);

            if (userEntity is null)
            {
                return false;
            }

            if (chargeValue > userEntity.Balance.Balance)
            {
                return false;
            }

            userEntity.Balance.Balance -= chargeValue;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<UserModel> GetUserByIdAsync(int userId)
        {
            var userEntity = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);

            if (userEntity is null)
            {
                return null;
            }

            return _mapper.Map<UserModel>(userEntity);
        }

        public async Task<UserModel> GetUserByLoginAsync(string login)
        {
            var userEntity = await _context.Users.FirstOrDefaultAsync(x => x.Login == login);

            if (userEntity is null)
            {
                return null;
            }

            return _mapper.Map<UserModel>(userEntity);
        }

        public async Task<UserModel> GetUserByLoginPassMatchAsync(string login, string password)
        {
            var userEntity = await _context.Users.FirstOrDefaultAsync(x => x.Login == login && x.Password == password);

            if (userEntity is null)
            {
                return null;
            }

            return _mapper.Map<UserModel>(userEntity);
        }
    }
}
