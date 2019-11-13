using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MovieShop.Entity;

namespace MovieShop.Service
{
    public interface IUserService
    {
        Task<User> ValidateUser(string email, string password);

        Task<User> CreateUser(string email, string password, string firstName, string lastName);

        Task<IEnumerable<Purchase>> GetPurchasedMovies(int userid);

    }
}
