using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MovieShop.Data;
using MovieShop.Entity;

namespace MovieShop.Service
{
    public interface IMovieService
    {
        Task<IEnumerable<Movie>> GetHighestGrossingMovies();
    }
}
