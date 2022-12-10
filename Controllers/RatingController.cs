using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Globalization;
using WebApi3.Domain;
using WebApi3.Repositories;

namespace Dot.Net.WebApi.Controllers
{
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    [Route("[controller]")]
    public class RatingController : Controller
    {
        // TODO: Inject Rating repository
        private readonly IRatingRepository _ratingRepository; 
        private readonly ILogger<RatingController> _logger;

        public RatingController(IRatingRepository ratingRepository, ILogger<RatingController> logger)
        {
            _ratingRepository = ratingRepository;
            _logger = logger;
        }

        /// <summary>
        /// Method to display the entire list of Ratings
        /// </summary>
        /// <returns></returns>
        [HttpGet("/rating/list")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Home()
        {
            // Add Logs with date/time in US standard
            _logger.LogInformation("User requested the list of Ratings on {Date} at {Time}", DateTime.UtcNow.ToString("dddd, MMMM d, yyyy", CultureInfo.InvariantCulture), DateTime.UtcNow.ToString("h:mm:ss tt", CultureInfo.InvariantCulture));
            // Add Logs with date/time in French standard
            //_logger.LogInformation("User requested the list of Ratings on {Date} {Time}", DateTime.UtcNow.ToLongDateString(), DateTime.UtcNow.ToLongTimeString());

            var result = _ratingRepository.GetAllRatings();
            if (result.Result == null)
            {
                _logger.LogError("There is no Rating to display.");
                return NotFound("No Rating to display.");
            }
            
            return Ok(result.Result);
        }

        /// <summary>
        /// Method to display a single Rating, identified by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("/rating/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetSpecificRating(int id)
        {
            _logger.LogInformation("User requested the Rating with Id = {Id} on {Date} at {Time}", id, DateTime.UtcNow.ToString("dddd, MMMM d, yyyy", CultureInfo.InvariantCulture), DateTime.UtcNow.ToString("h:mm:ss tt", CultureInfo.InvariantCulture));

            var result = _ratingRepository.GetSingleRating(id);
            if (result.Result == null)
            {
                _logger.LogError("No Rating with Id = {Id} was found. User was advised to enter a valid Id.", id);
                return NotFound("Rating not found. Please enter an valid Id.");
            }

            return Ok(result.Result);
        }

        /// <summary>
        /// Method to add a Rating if the data is valid
        /// </summary>
        /// <param name="rating"></param>
        /// <returns></returns>
        [HttpPost("/rating/add")]
        public IActionResult AddRating([FromBody] Rating rating)
        {
            var result = _ratingRepository.CreateRating(rating);
            // If the Rating wasn't validated, return a Bad Request error
            if (result is null)
            {
                return BadRequest(result.Result);
            }

            _logger.LogInformation("User created a new Rating on {Date} at {Time}", DateTime.UtcNow.ToString("dddd, MMMM d, yyyy", CultureInfo.InvariantCulture), DateTime.UtcNow.ToString("h:mm:ss tt", CultureInfo.InvariantCulture));
            return Ok(result.Result);
        }

        /// <summary>
        /// Method to update a given Rating, identified by its id
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="rating"></param>
        /// <returns></returns>
        [HttpPut("/rating/update/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateRatingt(int id, [FromBody] Rating rating)
        {
            if (id != rating.Id)
            {
                return BadRequest("Ids should be identical.");
            }

            var result = _ratingRepository.UpdateRating(id, rating);
            if (result.Result is null)
            {
                //_logger.LogInformation("User requested to update the Rating with Id = {Id} on {Date} at {Time}", id, DateTime.UtcNow.ToString("dddd, MMMM d, yyyy", CultureInfo.InvariantCulture), DateTime.UtcNow.ToString("h:mm:ss tt", CultureInfo.InvariantCulture));
                //_logger.LogError("No Rating with Id = {Id} was found. User was advised to enter a valid Id.", id);
                return BadRequest("Rating not found. Please enter a valid Id.");
            }

            _logger.LogInformation("User updated the Rating with Id = {Id} on {Date} at {Time}", id, DateTime.UtcNow.ToString("dddd, MMMM d, yyyy", CultureInfo.InvariantCulture), DateTime.UtcNow.ToString("h:mm:ss tt", CultureInfo.InvariantCulture));
            return Ok(result.Result);
        }

        /// <summary>
        /// Method to delete a given Rating, identified by its id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete("/rating/delete/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteRating(int id)
        {
            var result = _ratingRepository.DeleteRating(id);
            if (result.Result == null)
            {
                _logger.LogInformation("User requested to delete the Rating with Id = {Id} on {Date} at {Time}", id, DateTime.UtcNow.ToString("dddd, MMMM d, yyyy", CultureInfo.InvariantCulture), DateTime.UtcNow.ToString("h:mm:ss tt", CultureInfo.InvariantCulture));
                _logger.LogError("No Rating with Id = {Id} was found. User was advised to enter a valid Id.", id);
                return NotFound("Rating not found. Please enter a valid Id.");
            }

            _logger.LogInformation("User deleted the Rating with Id = {Id} on {Date} at {Time}", id, DateTime.UtcNow.ToString("dddd, MMMM d, yyyy", CultureInfo.InvariantCulture), DateTime.UtcNow.ToString("h:mm:ss tt", CultureInfo.InvariantCulture));
            return Ok(result.Result);
        }
    }
}