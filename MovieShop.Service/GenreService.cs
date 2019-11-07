using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MovieShop.Entity;
using MovieShop.Data;
using System.Linq;

namespace MovieShop.Service
{
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _genreRepository;
        public GenreService(IGenreRepository genreRepository)
        {
            this._genreRepository = genreRepository;
        }
        
        public async Task<IEnumerable<Genre>> GetAllGenres()
        {
            //throw new NotImplementedException();
            var result = await _genreRepository.GetAllAsync();
            return result.OrderBy(g => g.Name); 
        }

        public async Task<IEnumerable<Movie>> GetMovieByGenre(int genreid)
        {
            var movies = await _genreRepository.GetMoviesByGenre(genreid);
            return movies;
        }
    }
}
