using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Globalization;
using WebApi3.Domain.DTO;
using WebApi3.Repositories;

namespace Dot.Net.WebApi.Controllers
{
    [Route("[controller]")]
    public class CurveController : Controller
    {
        // TODO: Inject Curve Point service
        private readonly ICurvePointRepository _curvePointService;
        private readonly ILogger<CurveController> _logger;

        public CurveController(ICurvePointRepository curvePointService, ILogger<CurveController> logger)
        {
            _curvePointService = curvePointService;
            _logger = logger;
        }

        /// <summary>
        /// Method to display the entire list of CurevPoints
        /// </summary>
        /// <returns></returns>
        [HttpGet("/curvePoint/list")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Home()
        {
            // Add Logs with date/time in US standard
            _logger.LogInformation("User requested the list of CurvePoints on {Date} at {Time}", DateTime.UtcNow.ToString("dddd, MMMM d, yyyy", CultureInfo.InvariantCulture), DateTime.UtcNow.ToString("h:mm:ss tt", CultureInfo.InvariantCulture));
            // Add Logs with date/time in French standard
            //_logger.LogInformation("User requested the list of CurvePoints on {Date} {Time}", DateTime.UtcNow.ToLongDateString(), DateTime.UtcNow.ToLongTimeString());
            var result = _curvePointService.GetAllCurvePoints();
            if (result.Result == null)
            {
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
            _logger.LogInformation("User requested the CurvePoint with Id = {Id} on {Date} at {Time}", id, DateTime.UtcNow.ToString("dddd, MMMM d, yyyy", CultureInfo.InvariantCulture), DateTime.UtcNow.ToString("h:mm:ss tt", CultureInfo.InvariantCulture));

            var result = _curvePointService.GetSingleCurvePoint(id);
            if (result.Result == null)
            {
                return NotFound("CurvePoint not found. Enter an valid Id.");
            }
            
            return Ok(result.Result);
        }

        /// <summary>
        /// Method to add a curvePoint if the data is valid
        /// </summary>
        /// <param name="curvePoint"></param>
        /// <returns></returns>
        [HttpPost("/curvePoint/add")]
        public IActionResult AddCurvePoint([FromBody]CurvePointDTO curvePointDTO)
        {
            _logger.LogInformation("User created a new CurvePoint on {Date} at {Time}", DateTime.UtcNow.ToString("dddd, MMMM d, yyyy", CultureInfo.InvariantCulture), DateTime.UtcNow.ToString("h:mm:ss tt", CultureInfo.InvariantCulture));
            var result = _curvePointService.CreateCurvePoint(curvePointDTO);
            // If the curvePoint wasn't validated by the service, return a Bad Request error
            if (result is null)
            {
                return BadRequest();
            }
            return Ok(result.Result);
        }

        /// <summary>
        /// Method to update a given CurvePoint, identified by its id
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="curvePointDTO"></param>
        /// <returns></returns>
        [HttpPut("/CurvePoint/update/{id}")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateCurvePoint(int id, [FromBody]CurvePointDTO curvePointDTO)
        {
            if (id != curvePointDTO.Id)
            {
                return BadRequest("Ids should be identical.");
            }

            var result = _curvePointService.UpdateCurvePoint(id, curvePointDTO);
            if (result.Result is null)
            {
                return BadRequest("CurvePoint not found."); 
            }

            _logger.LogInformation("User updated the CurvePoint with Id = {Id} on {Date} at {Time}", id, DateTime.UtcNow.ToString("dddd, MMMM d, yyyy", CultureInfo.InvariantCulture), DateTime.UtcNow.ToString("h:mm:ss tt", CultureInfo.InvariantCulture));
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
            var result = _curvePointService.DeleteCurvePoint(id);
            if (result.Result == null)
            {
                return NotFound("CurvePoint not found.");
            }

            _logger.LogInformation("User deleted the CurvePoint with Id = {Id} on {Date} at {Time}", id, DateTime.UtcNow.ToString("dddd, MMMM d, yyyy", CultureInfo.InvariantCulture), DateTime.UtcNow.ToString("h:mm:ss tt", CultureInfo.InvariantCulture));
            return Ok(result.Result);
        }



        //ORIGINAL CODE
        //[HttpGet("/curvePoint/add")]
        //public IActionResult Validate([FromBody]CurvePoint curvePoint)
        //{
        //    // TODO: check data valid and save to db, after saving return bid list
        //    return View("curvePoint/add"    );
        //}

        //[HttpGet("/curvePoint/update/{id}")]
        //public IActionResult ShowUpdateForm(int id)
        //{
        //    // TODO: get CurvePoint by Id and to model then show to the form
        //    return View("curvepoint/update");
        //}

        //[HttpPost("/curvepoint/update/{id}")]
        //public IActionResult UpdateCurvePoint(int id, [FromBody] CurvePoint curvePoint)
        //{
        //    // TODO: check required fields, if valid call service to update Curve and return Curve list
        //    return Redirect("/curvepoint/list");
        //}

        //[HttpDelete("/curvepoint/{id}")]
        //public IActionResult DeleteBid(int id)
        //{
        //    // TODO: Find Curve by Id and delete the Curve, return to Curve list

        //    return Redirect("/curvePoint/list");
        //}
    }
}