using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using WebApi3.Repositories;
using Microsoft.Extensions.Configuration;
using System;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Logging;
using System.Globalization;

namespace Dot.Net.WebApi.Controllers
{
    [Route("[controller]")]
    public class LoginController : Controller
    {
        public IConfiguration _configuration;
        private readonly ILoginRepository _loginRepository;
        private readonly ILogger<LoginController> _logger;

        public LoginController(IConfiguration config, ILoginRepository loginRepository, ILogger<LoginController> logger)
        {
            _configuration = config;
            _loginRepository = loginRepository;
            _logger = logger;
        }

        /// <summary>
        /// Method for a user to login to the API
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpPost("/login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Login(string userName, string password)
        {
            if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(password))
            {
                var loggedInUser = _loginRepository.Login(userName, password);

                if (loggedInUser == null)
                {
                    return BadRequest("Invalid UserName or Password.");
                }

                var claims = new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, loggedInUser.UserName),
                };

                //Define the token
                var token = new JwtSecurityToken
                (
                    issuer: _configuration["Jwt:Issuer"], 
                    audience: _configuration["Jwt:Audience"],
                    claims: claims,
                    expires: DateTime.UtcNow.AddMinutes(60),
                    notBefore: DateTime.UtcNow,
                    signingCredentials: new SigningCredentials(
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])),
                        SecurityAlgorithms.HmacSha256)
                );

                var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

                _logger.LogInformation("{User} Logged in on {Date} at {Time}", loggedInUser.UserName, DateTime.UtcNow.ToString("dddd, MMMM d, yyyy", CultureInfo.InvariantCulture), DateTime.UtcNow.ToString("h:mm:ss tt", CultureInfo.InvariantCulture));

                return Ok(tokenString);
            }

            return BadRequest("Please enter a UserName and a Password");
        }
    }
}