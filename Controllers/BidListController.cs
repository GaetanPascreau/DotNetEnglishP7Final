using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Globalization;
using WebApi3.Domain;
using WebApi3.Domain.DTO;
using WebApi3.Repositories;

namespace Dot.Net.WebApi.Controllers
{
    [Route("[controller]")]
    public class BidListController : Controller
    {
        // TODO: Inject BidList repository
        private readonly IBidListRepository _bidListRepository;
        private readonly ILogger<BidListController> _logger;

        public BidListController(IBidListRepository bidListRepository, ILogger<BidListController> logger)
        {
            _bidListRepository = bidListRepository;
            _logger = logger;
        }

        /// <summary>
        /// Method to display the entire list of BidList
        /// </summary>
        /// <returns></returns>
        [HttpGet("/bidList/list")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Home()
        {
            // Add Logs with date/time in US standard
            _logger.LogInformation("User requested the list of BidList on {Date} at {Time}", DateTime.UtcNow.ToString("dddd, MMMM d, yyyy", CultureInfo.InvariantCulture), DateTime.UtcNow.ToString("h:mm:ss tt", CultureInfo.InvariantCulture));
            // Add Logs with date/time in French standard
            //_logger.LogInformation("User requested the list of BidList on {Date} {Time}", DateTime.UtcNow.ToLongDateString(), DateTime.UtcNow.ToLongTimeString());
            
            var result = _bidListRepository.GetAllBidLists();
            if (result.Result == null)
            {
                _logger.LogError("There is no BidList to display.");
                return NotFound("No BidList to display.");
            }

            return Ok(result.Result);
        }

        /// <summary>
        /// Method to display a single BidList, identified by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("/bidList/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetSpecificBidList(int id)
        {
            _logger.LogInformation("User requested the BidList with Id = {Id} on {Date} at {Time}", id, DateTime.UtcNow.ToString("dddd, MMMM d, yyyy", CultureInfo.InvariantCulture), DateTime.UtcNow.ToString("h:mm:ss tt", CultureInfo.InvariantCulture));

            var result = _bidListRepository.GetSingleBidList(id);
            if (result.Result == null)
            {
                _logger.LogError("No BidList with Id = {Id} was found. User was advised to enter a valid Id.", id);
                return NotFound("BidList not found. Enter an valid Id.");
            }

            return Ok(result.Result);
        }

        /// <summary>
        /// Method to add a BidList if the data is valid
        /// </summary>
        /// <param name="bidList"></param>
        /// <returns></returns>
        [HttpPost("/bidList/add")]
        public IActionResult AddBidList([FromBody] BidListDTO bidListDTO)
        {
            var result = _bidListRepository.CreateBidList(bidListDTO);
            // If the BidList wasn't validated by the service, return a Bad Request error
            if (result is null)
            {
                return BadRequest();
            }

            _logger.LogInformation("User created a new BidList on {Date} at {Time}", DateTime.UtcNow.ToString("dddd, MMMM d, yyyy", CultureInfo.InvariantCulture), DateTime.UtcNow.ToString("h:mm:ss tt", CultureInfo.InvariantCulture));
            return Ok(result.Result);
        }

        /// <summary>
        /// Method to update a given BidList, identified by its id
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="bidListDTO"></param>
        /// <returns></returns>
        [HttpPut("/bidList/update/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateCurvePoint(int id, [FromBody] BidListDTO bidListDTO)
        {
            if (id != bidListDTO.BidListId)
            {
                return BadRequest("Ids should be identical.");
            }

            var result = _bidListRepository.UpdateBidList(id, bidListDTO);
            if (result.Result is null)
            {
                return BadRequest("BidList not found.");
            }

            _logger.LogInformation("User updated the BidList with Id = {Id} on {Date} at {Time}", id, DateTime.UtcNow.ToString("dddd, MMMM d, yyyy", CultureInfo.InvariantCulture), DateTime.UtcNow.ToString("h:mm:ss tt", CultureInfo.InvariantCulture));
            return Ok(result.Result);
        }

        /// <summary>
        /// Method to delete a given BidList, identified by its id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete("/bidList/delete/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteBidList(int id)
        {
            var result = _bidListRepository.DeleteBidList(id);
            if (result.Result == null)
            {
                _logger.LogInformation("User requested to delete the BidList with Id = {Id} on {Date} at {Time}", id, DateTime.UtcNow.ToString("dddd, MMMM d, yyyy", CultureInfo.InvariantCulture), DateTime.UtcNow.ToString("h:mm:ss tt", CultureInfo.InvariantCulture));
                _logger.LogError("No BidList with Id = {Id} was found. User was advised to enter a valid Id.", id);
                return NotFound("BidList not found. Please enter a vlid Id.");
            }

            _logger.LogInformation("User deleted the BidList with Id = {Id} on {Date} at {Time}", id, DateTime.UtcNow.ToString("dddd, MMMM d, yyyy", CultureInfo.InvariantCulture), DateTime.UtcNow.ToString("h:mm:ss tt", CultureInfo.InvariantCulture));
            return Ok(result.Result);
        }
    }
}