using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieShop.Service;
using MovieShop.Entity;
using MovieShop.API.DTO_data_transfer_object_;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;

namespace MovieShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IUserService _userService;
        public UserController(IUserService userService, IConfiguration config)
        {
            this._userService = userService;
            this._config = config;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUserAsync([FromBody]CreateUserDTO createuserDTO)
        {
            if(createuserDTO == null || string.IsNullOrEmpty(createuserDTO.email) || string.IsNullOrEmpty(createuserDTO.password))
            {
                return BadRequest();
            }
            var user = await _userService.CreateUser(createuserDTO.email, createuserDTO.password,
                createuserDTO.firstName, createuserDTO.lastName);
            if(user == null)
            {
                return BadRequest("Email already exist");
            }
            return Ok("Created Successfully");
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> ValidationAccountAsync([FromBody] CreateUserDTO userDTO)
        {
            var user = await _userService.ValidateUser(userDTO.email, userDTO.password);
            if(user == null)
            {
                return Unauthorized("Something happens");
            }
            return Ok(new { token = GenerateToken(user)});
        }

        [HttpGet]
        [Authorize]
        [Route("{id}/purchases")]
        public async Task<IActionResult> GetUserPurchasedMoviesAsync(int id)
        {
            var usermovies = await _userService.GetPurchasedMovies(id);
            return Ok(usermovies);
        }

        private string GenerateToken(User user)
        {
            //claims info in payload 
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName),
                new Claim("alias", user.FirstName[0] + user.LastName[0].ToString()),
                new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName)
            };
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["TokenSettings:PrivateKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_config["TokenSettings:ExpirationDays"]));
            

            //generate the token 
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = expires,
                SigningCredentials = credentials,
                Issuer = _config["TokenSettings:Issuer"],
                Audience = _config["TokenSettings:Audience"]
            };


            var encodedJwt = new JwtSecurityTokenHandler().CreateToken(tokenDescriptor);
            return new JwtSecurityTokenHandler().WriteToken(encodedJwt);
        }
    }
}