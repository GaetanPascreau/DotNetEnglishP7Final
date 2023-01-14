using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Globalization;
using WebApi3.Domain.DTO;
using WebApi3.Repositories;

namespace Dot.Net.WebApi.Controllers
{
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    [Route("[controller]")]
    public class CurvePointController : Controller
    {
        // TODO: Inject Curve Point repository
        private readonly ICurvePointRepository _curvePointRepository;
        private readonly ILogger<CurvePointController> _logger;

        public CurvePointController(ICurvePointRepository curvePointRepository, ILogger<CurvePointController> logger)
        {
            _curvePointRepository = curvePointRepository;
            _logger = logger;
        }

        /// <summary>
        /// Method to display the entire list of CurevPoints
        /// </summary>
        /// <returns></returns>
        [HttpGet("/curvePoint/list")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetAllCurvePoints()
        {
            // Add Logs with date/time in US standard
            _logger.LogInformation("User requested the list of CurvePoints on {Date} at {Time} (UTC)", DateTime.UtcNow.ToString("dddd, MMMM d, yyyy", CultureInfo.InvariantCulture), DateTime.UtcNow.ToString("h:mm:ss tt", CultureInfo.InvariantCulture));
            // Add Logs with date/time in French standard
            //_logger.LogInformation("User requested the list of CurvePoints on {Date} {Time}", DateTime.UtcNow.ToLongDateString(), DateTime.UtcNow.ToLongTimeString());
           
            var result = _curvePointRepository.GetAllCurvePoints();
            if (result.Result == null)
            {
                _logger.LogError("There is no CurvePoint to display.");
                return NotFound("No CurvePoints to display.");
            }

            return Ok(result.Result);
        }

        /// <summary>
        /// Method to display a single CurvePoint, identified by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("/curvePoint/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetSpecificCurvePoint(int id)
        {
            _logger.LogInformation("User requested the CurvePoint with Id = {Id} on {Date} at {Time} (UTC)", id, DateTime.UtcNow.ToString("dddd, MMMM d, yyyy", CultureInfo.InvariantCulture), DateTime.UtcNow.ToString("h:mm:ss tt", CultureInfo.InvariantCulture));

            var result = _curvePointRepository.GetSingleCurvePoint(id);
            if (result.Result == null)
            {
                _logger.LogError("No CurvePoint with Id = {Id} was found. User was advised to enter a valid Id.", id);
                return NotFound("CurvePoint not found. Please enter a valid Id.");
            }
            
            return Ok(result.Result);
        }

        /// <summary>
        /// Method to add a CurvePoint if the data is valid
        /// </summary>
        /// <param name="curvePointDTO"></param>
        /// <returns></returns>
        [HttpPost("/curvePoint/add")]
        public IActionResult AddCurvePoint([FromBody]CurvePointDTO curvePointDTO)
        {
            var result = _curvePointRepository.CreateCurvePoint(curvePointDTO);
            // If the curvePoint wasn't validated, return a Bad Request error
            if (result is null)
            {
                return BadRequest(result.Result);
            }

            _logger.LogInformation("User created a new CurvePoint on {Date} at {Time} (UTC)", DateTime.UtcNow.ToString("dddd, MMMM d, yyyy", CultureInfo.InvariantCulture), DateTime.UtcNow.ToString("h:mm:ss tt", CultureInfo.InvariantCulture));
            return Ok(result.Result);
        }

        /// <summary>
        /// Method to update a given CurvePoint, identified by its id
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="curvePointDTO"></param>
        /// <returns></returns>
        [HttpPut("/CurvePoint/update/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateCurvePoint(int id, [FromBody]CurvePointDTO curvePointDTO)
        {
            if (id != curvePointDTO.Id)
            {
                return BadRequest("Ids should be identical.");
            }

            var result = _curvePointRepository.UpdateCurvePoint(id, curvePointDTO);
            if (result.Result is null)
            {
                return BadRequest("CurvePoint not found."); 
            }

            _logger.LogInformation("User updated the CurvePoint with Id = {Id} on {Date} at {Time} (UTC)", id, DateTime.UtcNow.ToString("dddd, MMMM d, yyyy", CultureInfo.InvariantCulture), DateTime.UtcNow.ToString("h:mm:ss tt", CultureInfo.InvariantCulture));
            return Ok(result.Result);            
        }

        /// <summary>
        /// Method to delete a given CurvePoint, identified by its id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete("/CurvePoint/delete/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteCurvePoint(int id)
        {
            var result = _curvePointRepository.DeleteCurvePoint(id);
            if (result.Result == null)
            {
                _logger.LogInformation("User requested to delete the CurvePoint with Id = {Id} on {Date} at {Time} (UTC)", id, DateTime.UtcNow.ToString("dddd, MMMM d, yyyy", CultureInfo.InvariantCulture), DateTime.UtcNow.ToString("h:mm:ss tt", CultureInfo.InvariantCulture));
                _logger.LogError("No CurvePoint with Id = {Id} was found. User was advised to enter a valid Id.", id);
                return NotFound("CurvePoint not found.");
            }

            _logger.LogInformation("User deleted the CurvePoint with Id = {Id} on {Date} at {Time} (UTC)", id, DateTime.UtcNow.ToString("dddd, MMMM d, yyyy", CultureInfo.InvariantCulture), DateTime.UtcNow.ToString("h:mm:ss tt", CultureInfo.InvariantCulture));
            return Ok(result.Result);
        }
    }
}