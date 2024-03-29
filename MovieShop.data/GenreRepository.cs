﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MovieShop.Entity;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace MovieShop.Data
{
    public class GenreRepository:Repository<Genre> ,IGenreRepository
    {
        public GenreRepository(MovieShopDbcontext dbContext):base(dbContext)
        {

        }

        public async Task<IEnumerable<Movie>> GetMoviesByGenre(int genreid)
        {
            var movies = await _dbContext.MovieGenres.Where(g => g.GenreId == genreid).Include(m => m.Movie).ToListAsync();
            return movies.Select(m => m.Movie).ToList();
        }
    }
}
