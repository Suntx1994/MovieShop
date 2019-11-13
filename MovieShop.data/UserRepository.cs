using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using MovieShop.Entity;
using Microsoft.EntityFrameworkCore;


namespace MovieShop.Data
{
    public class UserRepository:Repository<User>,IUserRepository
    {
        public UserRepository(MovieShopDbcontext dbcontext):base(dbcontext)
        {

        }

        public async Task<User> GetUserByEmail(string email)
        {
            //throw new NotImplementedException();
            return await _dbContext.Users.SingleOrDefaultAsync(u => u.Email == email);
        }

        public async Task<IEnumerable<Purchase>> GetUserPurchasedMovies(int userid)
        {
            //throw new NotImplementedException();
            var userMovie = await _dbContext.purchases.Where(p => p.UserId == userid).Include(p => p.Movie).ToListAsync();
            return userMovie;
        }
    }
}
