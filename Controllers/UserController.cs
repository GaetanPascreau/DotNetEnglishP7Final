using System;
using System.Globalization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApi3.Domain.DTO;
using WebApi3.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Dot.Net.WebApi.Controllers
{
    
    [Route("[controller]")]
    public class UserController : Controller
    {
        // TODO: Inject User repository
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserRepository userRepository, ILogger<UserController> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        /// <summary>
        /// Method to display the entire list of Users
        /// </summary>
        /// <returns></returns>
        [HttpGet("/user/list")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetAllUsers()
        {
            // Add Logs with date/time in US standard
            _logger.LogInformation("User requested the list of Users on {Date} at {Time} (UTC)", DateTime.UtcNow.ToString("dddd, MMMM d, yyyy", CultureInfo.InvariantCulture), DateTime.UtcNow.ToString("h:mm:ss tt", CultureInfo.InvariantCulture));
            // Add Logs with date/time in French standard
            //_logger.LogInformation("User requested the list of Users on {Date} {Time}", DateTime.UtcNow.ToLongDateString(), DateTime.UtcNow.ToLongTimeString());

            var result = _userRepository.GetAllUsers();
            if (result.Result == null)
            {
                _logger.LogError("There is no User to display.");
                return NotFound("No User to display.");
            }

            return Ok(result.Result);
        }

        /// <summary>
        /// Method to display a single User, identified by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("/user/{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetSpecificUser(int id)
        {
            _logger.LogInformation("User requested the User with Id = {Id} on {Date} at {Time} (UTC)", id, DateTime.UtcNow.ToString("dddd, MMMM d, yyyy", CultureInfo.InvariantCulture), DateTime.UtcNow.ToString("h:mm:ss tt", CultureInfo.InvariantCulture));

            var result = _userRepository.GetSingleUser(id);
            if (result.Result == null)
            {
                _logger.LogError("No User with Id = {Id} was found. User was advised to enter a valid Id.", id);
                return NotFound("User not found. Please enter a valid Id.");
            }

            return Ok(result.Result);
        }

        /// <summary>
        /// Method to display a single User, identified by its UserName
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        [HttpGet("/userByUserName/")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetSpecificUserByUserName(string userName)
        {
            _logger.LogInformation("User requested the User with UserName {UserName} on {Date} at {Time} (UTC)", userName, DateTime.UtcNow.ToString("dddd, MMMM d, yyyy", CultureInfo.InvariantCulture), DateTime.UtcNow.ToString("h:mm:ss tt", CultureInfo.InvariantCulture));

            var result = _userRepository.GetSingleUserByUserName(userName);
            if (result.Result == null)
            {
                _logger.LogError("No User with UserName {UserName} was found. User was advised to enter a valid UserName.", userName);
                return NotFound("User not found. Please enter a valid UserName.");
            }

            return Ok(result.Result);
        }

        /// <summary>
        /// Method to add a User if the data is valid
        /// </summary>
        /// <param name="userDTO"></param>
        /// <returns></returns>
        [HttpPost("/user/add")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AddUser([FromBody] UserDTO userDTO)
        {
            var result = _userRepository.CreateUser(userDTO);
            // If the User wasn't validated, or if that user already exists in database, return a Bad Request error
            if (result.Result is null)
            {
                return BadRequest("UserName or FullName is already used");
            }

            _logger.LogInformation("User created a new User on {Date} at {Time} (UTC)", DateTime.UtcNow.ToString("dddd, MMMM d, yyyy", CultureInfo.InvariantCulture), DateTime.UtcNow.ToString("h:mm:ss tt", CultureInfo.InvariantCulture));
            return Ok(result.Result);
        }

        /// <summary>
        /// Method to update a given User, identified by its id
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="userDTO"></param>
        /// <returns></returns>
        [HttpPut("/user/update/{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateUser(int id, [FromBody] UserDTO userDTO)
        {
            if (id != userDTO.Id)
            {
                return BadRequest("Ids should be identical.");
            }

            var result = _userRepository.UpdateUser(id, userDTO);
            if (result.Result is null)
            {
                return BadRequest("User not found.");
            }

            _logger.LogInformation("User updated the User with Id = {Id} on {Date} at {Time} (UTC)", id, DateTime.UtcNow.ToString("dddd, MMMM d, yyyy", CultureInfo.InvariantCulture), DateTime.UtcNow.ToString("h:mm:ss tt", CultureInfo.InvariantCulture));
            return Ok(result.Result);
        }

        /// <summary>
        /// Method to delete a given User, identified by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("/user/delete/{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteUser(int id)
        {
            var result = _userRepository.DeleteUser(id);
            if (result.Result == null)
            {
                _logger.LogInformation("User requested to delete the User with Id = {Id} on {Date} at {Time} (UTC)", id, DateTime.UtcNow.ToString("dddd, MMMM d, yyyy", CultureInfo.InvariantCulture), DateTime.UtcNow.ToString("h:mm:ss tt", CultureInfo.InvariantCulture));
                _logger.LogError("No User with Id = {Id} was found. User was advised to enter a valid Id.", id);
                return NotFound("User not found.");
            }

            _logger.LogInformation("User deleted the User with Id = {Id} on {Date} at {Time} (UTC)", id, DateTime.UtcNow.ToString("dddd, MMMM d, yyyy", CultureInfo.InvariantCulture), DateTime.UtcNow.ToString("h:mm:ss tt", CultureInfo.InvariantCulture));
            return Ok(result.Result);
        }
    }
}