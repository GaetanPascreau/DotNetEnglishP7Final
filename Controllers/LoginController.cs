using Microsoft.AspNetCore.Mvc;
using WebApi3.Domain;
using WebApi3.Repositories;

namespace Dot.Net.WebApi.Controllers
{
    [Route("[controller]")]
    public class LoginController : Controller
    {
        //private UserRepository _userRepository;
        private readonly ILoginRepository _loginRepository;

        //public LoginController(UserRepository userRepository)
        //{
        //    _userRepository = userRepository;
        //}

        public LoginController(ILoginRepository loginRepository)
        {
            _loginRepository = loginRepository;
        }

        [HttpPost("/login")]
        public IActionResult Login(string userName, string password)
        {
            var result = _loginRepository.Login(userName, password);
            if(result == false)
            {
                return BadRequest("Invalid UserName or Password.");
            }
            return Ok("you are granted access to the API.");
        }
        //[HttpGet("/login")]
        //public IActionResult Login()
        //{
        //    return View("login");
        //}

        //[HttpGet("/secure/article-details")]
        //public IActionResult GetAllUserArticles()
        //{
        //    return View(_userRepository.GetAllUsers());
        //}

        //[HttpGet("/error")]
        //public IActionResult Error()
        //{
        //    string errorMessage= "You are not authorized for the requested data.";

        //    return View(new UnauthorizedObjectResult(errorMessage));
        //}

        ////[HttpGet("/trade/update/{id}")]
        ////public IActionResult ShowUpdateForm(int id)
        ////{
        ////    // TODO: get Trade by Id and to model then show to the form
        ////    return View("trade/update");
        ////}

        //[HttpPost("/trade/update/{id}")]
        //public IActionResult updateTrade(int id, [FromBody] Trade rating)
        //{
        //    // TODO: check required fields, if valid call service to update Trade and return Trade list
        //    return Redirect("/trade/list");
        //}

        //[HttpDelete("/trade/{id}")]
        //public IActionResult DeleteTrade(int id)
        //{
        //    // TODO: Find Trade by Id and delete the Trade, return to Trade list
        //    return Redirect("/trade/list");
        //}
    }
}