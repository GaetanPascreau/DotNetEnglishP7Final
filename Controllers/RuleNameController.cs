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
    public class RuleNameController : Controller
    {
        // TODO: Inject RuleName service
        private readonly IRuleNameRepository _ruleNameRepository;
        private readonly ILogger<RuleNameController> _logger;

        public RuleNameController(IRuleNameRepository ruleNameRepository, ILogger<RuleNameController> logger)
        {
            _ruleNameRepository = ruleNameRepository;
            _logger = logger;
        }

        /// <summary>
        /// Method to display the entire list of RuleNames
        /// </summary>
        /// <returns></returns>
        [HttpGet("/ruleName/list")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Home()
        {
            // Add Logs with date/time in US standard
            _logger.LogInformation("User requested the list of RuleNames on {Date} at {Time}", DateTime.UtcNow.ToString("dddd, MMMM d, yyyy", CultureInfo.InvariantCulture), DateTime.UtcNow.ToString("h:mm:ss tt", CultureInfo.InvariantCulture));
            // Add Logs with date/time in French standard
            //_logger.LogInformation("User requested the list of CurvePoints on {Date} {Time}", DateTime.UtcNow.ToLongDateString(), DateTime.UtcNow.ToLongTimeString());

            var result = _ruleNameRepository.GetAllRuleNames();
            if (result.Result == null)
            {
                _logger.LogError("There is no RuleName to display.");
                return NotFound("No RuleName to display.");
            }

            return Ok(result.Result);
        }

        /// <summary>
        /// Method to display a single RuleName, identified by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("/ruleName/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetSpecificRuleName(int id)
        {
            _logger.LogInformation("User requested the RuleName with Id = {Id} on {Date} at {Time}", id, DateTime.UtcNow.ToString("dddd, MMMM d, yyyy", CultureInfo.InvariantCulture), DateTime.UtcNow.ToString("h:mm:ss tt", CultureInfo.InvariantCulture));

            var result = _ruleNameRepository.GetSingleRuleName(id);
            if (result.Result == null)
            {
                _logger.LogError("No RuleName with Id = {Id} was found. User was advised to enter a valid Id.", id);
                return NotFound("RuleName not found. Enter an valid Id.");
            }

            return Ok(result.Result);
        }

        /// <summary>
        /// Method to add a RuleName if the data is valid
        /// </summary>
        /// <param name="ruleName"></param>
        /// <returns></returns>
        [HttpPost("/ruleName/add")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AddRuleName([FromBody] RuleNameDTO ruleNameDTO)
        {
            var result = _ruleNameRepository.CreateRuleName(ruleNameDTO);
            // If the RuleName wasn't validated, return a Bad Request error
            if (result is null)
            {
                return BadRequest(result.Result);
            }

            _logger.LogInformation("User created a new RuleName on {Date} at {Time}", DateTime.UtcNow.ToString("dddd, MMMM d, yyyy", CultureInfo.InvariantCulture), DateTime.UtcNow.ToString("h:mm:ss tt", CultureInfo.InvariantCulture));
            return Ok(result.Result);
        }

        /// <summary>
        /// Method to update a given RuleName, identified by its id
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="ruleNameDTO"></param>
        /// <returns></returns>
        [HttpPut("/ruleName/update/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateRuleName(int id, [FromBody] RuleNameDTO ruleNameDTO)
        {
            if (id != ruleNameDTO.Id)
            {
                return BadRequest("Ids should be identical.");
            }

            var result = _ruleNameRepository.UpdateRuleName(id, ruleNameDTO);
            if (result.Result is null)
            {
                return BadRequest("RuleName not found.");
            }

            _logger.LogInformation("User updated the RuleName with Id = {Id} on {Date} at {Time}", id, DateTime.UtcNow.ToString("dddd, MMMM d, yyyy", CultureInfo.InvariantCulture), DateTime.UtcNow.ToString("h:mm:ss tt", CultureInfo.InvariantCulture));
            return Ok(result.Result);
        }

        /// <summary>
        /// Method to delete a given RuleName, identified by its id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete("/ruleName/delete/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteRuleName(int id)
        {
            var result = _ruleNameRepository.DeleteRuleName(id);
            if (result.Result == null)
            {
                _logger.LogInformation("User requested to delete the RuleName with Id = {Id} on {Date} at {Time}", id, DateTime.UtcNow.ToString("dddd, MMMM d, yyyy", CultureInfo.InvariantCulture), DateTime.UtcNow.ToString("h:mm:ss tt", CultureInfo.InvariantCulture));
                _logger.LogError("No RuleName with Id = {Id} was found. User was advised to enter a valid Id.", id);
                return NotFound("Rulename not found.");
            }

            _logger.LogInformation("User deleted the RuleName with Id = {Id} on {Date} at {Time}", id, DateTime.UtcNow.ToString("dddd, MMMM d, yyyy", CultureInfo.InvariantCulture), DateTime.UtcNow.ToString("h:mm:ss tt", CultureInfo.InvariantCulture));
            return Ok(result.Result);
        }
    }
}