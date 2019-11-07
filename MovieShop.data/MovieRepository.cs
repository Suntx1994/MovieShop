using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using MovieShop.Entity;

namespace MovieShop.Data
{
    public class MovieRepository:Repository<Movie>,IMovieRepository
    {
        public MovieRepository(MovieShopDbcontext dbcontext):base(dbcontext)
        {

        }

        public async Task<IEnumerable<Movie>> GetHighestGrossingMovies()
        {
            return await _dbContext.Moives.OrderByDescending(m => m.Revenue).Take(25).ToListAsync(); 
        }
    }
}
