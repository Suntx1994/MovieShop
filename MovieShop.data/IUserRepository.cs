using System;
using System.Collections.Generic;
using System.Text;
using MovieShop.Entity;
using System.Threading.Tasks;

namespace MovieShop.Data
{
    public interface IUserRepository:IRepository<User>
    {
        Task<User> GetUserByEmail(string email);

        Task<IEnumerable<Purchase>> GetUserPurchasedMovies(int userid);
    }
}
