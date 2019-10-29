using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MovieShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    

    //controllerbase do not have view, MVC uses :Controller
    public class GenreController : ControllerBase
    {

        [HttpGet]
        [Route("")]
        public IActionResult GetAllGenres()
        {
            int a = 1; int b = 0;
            int c = a / b;
            var genres = new[]
                 {
                     new {Id = 1, Name = "Action"},
                     new {Id = 2, Name = "Comedy"},
                     new {Id = 2, Name = "Thriller"} };
            return Ok(genres);
        }
    }
}