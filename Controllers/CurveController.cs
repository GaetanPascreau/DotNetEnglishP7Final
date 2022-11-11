using Microsoft.AspNetCore.Mvc;
using WebApi3.Domain;
using WebApi3.Validators;

namespace Dot.Net.WebApi.Controllers
{
    [Route("[controller]")]
    public class CurveController : Controller
    {
        // TODO: Inject Curve Point service

        [HttpGet("/curvePoint/list")]
        public IActionResult Home()
        {
            return View("curvePoint/list");
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

        [HttpGet("/curvePoint/add")]
        public IActionResult Validate([FromBody]CurvePoint curvePoint)
        {
            // TODO: check data valid and save to db, after saving return bid list
            return View("curvePoint/add"    );
        }

        [HttpGet("/curvePoint/update/{id}")]
        public IActionResult ShowUpdateForm(int id)
        {
            // TODO: get CurvePoint by Id and to model then show to the form
            return View("curvepoint/update");
        }

        [HttpPost("/curvepoint/update/{id}")]
        public IActionResult UpdateCurvePoint(int id, [FromBody] CurvePoint curvePoint)
        {
            // TODO: check required fields, if valid call service to update Curve and return Curve list
            return Redirect("/curvepoint/list");
        }

        [HttpDelete("/curvepoint/{id}")]
        public IActionResult DeleteBid(int id)
        {
            // TODO: Find Curve by Id and delete the Curve, return to Curve list

            return Redirect("/curvePoint/list");
        }
    }
}