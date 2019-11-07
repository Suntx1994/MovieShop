using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieShop.Service;

namespace MovieShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    

    //controllerbase do not have view, MVC uses :Controller
    public class GenreController : ControllerBase
    {
        private readonly IGenreService _genreService;
        public GenreController(IGenreService genreService)
        {
            this._genreService = genreService; 
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAllGenres()
        {
            var genres = await _genreService.GetAllGenres();
            if (genres.Any())
            {
                return Ok(genres);
            }
            return NotFound();
        }

        [HttpGet]
        [Route("{id}/movies")]
        public async Task<IActionResult> GetMoviesByGenre(int id)
        {
            var movies = await _genreService.GetMovieByGenre(id);
            return Ok(movies);
        }
    }
}