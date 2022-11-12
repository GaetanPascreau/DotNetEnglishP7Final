using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi3.Domain;
using WebApi3.Domain.DTO;
using WebApi3.Services;
using WebApi3.Validators;

namespace Dot.Net.WebApi.Controllers
{
    [Route("[controller]")]
    public class CurveController : Controller
    {
        // TODO: Inject Curve Point service
        private readonly ICurvePointService _curvePointService;

        public CurveController(ICurvePointService curvePointService)
        {
            _curvePointService = curvePointService;
        }

        [HttpGet("/curvePoint/list")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Home()
        {
            var curvePoints = _curvePointService.GetCurvePoint();
            //if (curvePoints is empty )
            //{ return NotFound(); }
            return Ok(curvePoints.Result);
        }

        [HttpGet("/curvePoint/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetSpecificCurvePoint(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var curvePoint = _curvePointService.GetCurvePointById(id);
            return Ok(curvePoint.Result);
        }

        /// <summary>
        /// Method to add a curvePoint if the data is valid
        /// </summary>
        /// <param name="curvePoint"></param>
        /// <returns></returns>
        [HttpPost("/curvePoint/add")]
        public IActionResult AddCurvePoint([FromBody]CurvePoint curvePoint)
        {
            var validator = new CurvePointValidator();
            var result = validator.Validate(curvePoint);
            if (result.IsValid)
            {
                return Ok();
            }
            return BadRequest(result.Errors);
        }

        [HttpPut("/curvePoint/update/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateCurvePoint(int Id, CurvePointDTO curvePointDTO)
        {
            if (!_curvePointService.UpdateCurvePoint(Id, curvePointDTO))
            {
                return BadRequest();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteCurvePoint(int Id)
        {
            if (Id == 0)
            {
                return NotFound();
            }
            _curvePointService.DeleteCurvePoint(Id);
            return Ok();
        }

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