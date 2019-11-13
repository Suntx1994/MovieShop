using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MovieShop.Entity;
using MovieShop.Data;

namespace MovieShop.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ICryptoService _cryptoService;
        public UserService(IUserRepository userRepository, ICryptoService cryptoService)
        {
            this._userRepository = userRepository;
            this._cryptoService = cryptoService;
        }
        public async Task<User> CreateUser(string email, string password, string firstName, string lastName)
        {
            //throw new NotImplementedException();
            var dbuser = await _userRepository.GetUserByEmail(email);
            if (dbuser != null)
            {
                return null;
            }
            var salt = _cryptoService.CreateSalt();
            var hashedPassword = _cryptoService.HashPassword(password, salt);
            var user = new User {
                Email = email,
                FirstName = firstName,
                LastName = lastName,
                HashedPassword = hashedPassword,
                Salt = salt
            };
            var createdUser = await _userRepository.AddAsync(user);
            return createdUser;    
        }

        public async Task<IEnumerable<Purchase>> GetPurchasedMovies(int userid)
        {
            //throw new NotImplementedException();
            return await _userRepository.GetUserPurchasedMovies(userid);
        }

        public async Task<User> ValidateUser(string email, string password)
        {
            //throw new NotImplementedException();
            var dbuser = await _userRepository.GetUserByEmail(email);
            if (dbuser == null)
            {
                return null;
            }
            var usersalt = dbuser.Salt;
            var hashedPassword = dbuser.HashedPassword;
            var currentPassword = _cryptoService.HashPassword(password, usersalt);
            if (currentPassword != hashedPassword)
            {
                return null;
            }

            return dbuser;

        }
    }
}
