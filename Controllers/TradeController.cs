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
    public class TradeController : Controller
    {
        // TODO: Inject Trade service

        private readonly ITradeRepository _tradeRepository;
        private readonly ILogger<TradeController> _logger;

        public TradeController(ITradeRepository tradeRepository, ILogger<TradeController> logger)
        {
            _tradeRepository = tradeRepository;
            _logger = logger;
        }

        /// <summary>
        /// Method to display the entire list of Trades
        /// </summary>
        /// <returns></returns>
        [HttpGet("/trade/list")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Home()
        {
            // Add Logs with date/time in US standard
            _logger.LogInformation("User requested the list of Trades on {Date} at {Time}", DateTime.UtcNow.ToString("dddd, MMMM d, yyyy", CultureInfo.InvariantCulture), DateTime.UtcNow.ToString("h:mm:ss tt", CultureInfo.InvariantCulture));
            // Add Logs with date/time in French standard
            //_logger.LogInformation("User requested the list of Trades on {Date} {Time}", DateTime.UtcNow.ToLongDateString(), DateTime.UtcNow.ToLongTimeString());

            var result = _tradeRepository.GetAllTrades();
            if (result.Result == null)
            {
                _logger.LogError("There is no Trade to display.");
                return NotFound("No Trade to display.");
            }

            return Ok(result.Result);
        }

        /// <summary>
        /// Method to display a single Trade, identified by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("/trade/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetSpecificTrade(int id)
        {
            _logger.LogInformation("User requested the Trade with Id = {Id} on {Date} at {Time}", id, DateTime.UtcNow.ToString("dddd, MMMM d, yyyy", CultureInfo.InvariantCulture), DateTime.UtcNow.ToString("h:mm:ss tt", CultureInfo.InvariantCulture));

            var result = _tradeRepository.GetSingleTrade(id);
            if (result.Result == null)
            {
                _logger.LogError("No Trade with Id = {Id} was found. User was advised to enter a valid Id.", id);
                return NotFound("Trade not found. Enter an valid Id.");
            }

            return Ok(result.Result);
        }

        /// <summary>
        /// Method to add a Trade if the data is valid
        /// </summary>
        /// <param name="tradeDTO"></param>
        /// <returns></returns>
        [HttpPost("/trade/add")]
        public IActionResult AddTrade([FromBody] TradeDTO tradeDTO)
        {
            var result = _tradeRepository.CreateTrade(tradeDTO);
            // If the Trade wasn't validated, return a Bad Request error
            if (result is null)
            {
                return BadRequest(result.Result);
            }

            _logger.LogInformation("User created a new Trade on {Date} at {Time}", DateTime.UtcNow.ToString("dddd, MMMM d, yyyy", CultureInfo.InvariantCulture), DateTime.UtcNow.ToString("h:mm:ss tt", CultureInfo.InvariantCulture));
            return Ok(result.Result);
        }

        /// <summary>
        /// Method to update a given Trade, identified by its id
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="tradeDTO"></param>
        /// <returns></returns>
        [HttpPut("/trade/update/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateTrade(int id, [FromBody] TradeDTO tradeDTO)
        {
            if (id != tradeDTO.TradeId)
            {
                return BadRequest("Ids should be identical.");
            }

            var result = _tradeRepository.UpdateTrade(id, tradeDTO);
            if (result.Result is null)
            {
                return BadRequest("Trade not found.");
            }

            _logger.LogInformation("User updated the Trade with Id = {Id} on {Date} at {Time}", id, DateTime.UtcNow.ToString("dddd, MMMM d, yyyy", CultureInfo.InvariantCulture), DateTime.UtcNow.ToString("h:mm:ss tt", CultureInfo.InvariantCulture));
            return Ok(result.Result);
        }

        /// <summary>
        /// Method to delete a given Trade, identified by its id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete("/trade/delete/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteTrade(int id)
        {
            var result = _tradeRepository.DeleteTrade(id);
            if (result.Result == null)
            {
                _logger.LogInformation("User requested to delete the Trade with Id = {Id} on {Date} at {Time}", id, DateTime.UtcNow.ToString("dddd, MMMM d, yyyy", CultureInfo.InvariantCulture), DateTime.UtcNow.ToString("h:mm:ss tt", CultureInfo.InvariantCulture));
                _logger.LogError("No Trade with Id = {Id} was found. User was advised to enter a valid Id.", id);
                return NotFound("Trade not found.");
            }

            _logger.LogInformation("User deleted the Trade with Id = {Id} on {Date} at {Time}", id, DateTime.UtcNow.ToString("dddd, MMMM d, yyyy", CultureInfo.InvariantCulture), DateTime.UtcNow.ToString("h:mm:ss tt", CultureInfo.InvariantCulture));
            return Ok(result.Result);
        }
    }
}