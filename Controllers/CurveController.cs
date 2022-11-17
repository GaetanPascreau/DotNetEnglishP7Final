using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using WebApi3.Domain.DTO;
using WebApi3.Repositories;
using WebApi3.Services;

namespace Dot.Net.WebApi.Controllers
{
    [Route("[controller]")]
    public class CurveController : Controller
    {
        // TODO: Inject Curve Point service
        private readonly ICurvePointRepository _curvePointService;

        public CurveController(ICurvePointRepository curvePointService)
        {
            _curvePointService = curvePointService;
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
            var result = _curvePointService.GetAllCurvePoints();

            using (StreamWriter w = System.IO.File.AppendText("log.txt"))
            {
                Log("User requested the BidList", w);
            }

            using (StreamReader r = System.IO.File.OpenText("log.txt"))
            {
                DumpLog(r);
            }

            return Ok(result.Result);
            //or just : return _curvePointService.GetAllCurvePoints();
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
            var result = _curvePointService.GetSingleCurvePoint(id);
            if (result == null)
            {
                return NotFound("CurvePoint not found.");
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
            // if the curvePoint wasn't validated by the service, return a Bad Request error
            var result = _curvePointService.CreateCurvePoint(curvePointDTO);
            if (result is null)
            {
                // do we return a specific message coming from the validation step???
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
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateCurvePoint(int id, [FromBody]CurvePointDTO curvePointDTO)
        {
            if (id != curvePointDTO.Id)
            {
                return BadRequest("Ids should be identical.");
            }

            var result = _curvePointService.UpdateCurvePoint(id, curvePointDTO);
            if (result is null)
            {
                return BadRequest(); 
                // or return a status 404 Not Found => return NotFound("CurvePoint was not found.");
            }
                return NoContent();
            // or return a status 200 Ok => return Ok(result); ???
            
        }

        /// <summary>
        /// Method to delete a given CurvePoint, identified by its id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete("/CurvePoint/delete/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteCurvePoint(int Id)
        {
            var result = _curvePointService.DeleteCurvePoint(Id);
            if (result == null)
            {
                return NotFound("CurvePoint not found.");
            }
            return Ok(result.Result);
        }

        public static void Log(string logMessage, TextWriter w)
        {
            w.Write("\r\nLog Entry : ");
            w.WriteLine($"{DateTime.Now.ToLongTimeString()} {DateTime.Now.ToLongDateString()}");
            w.WriteLine("  :");
            w.WriteLine($"  :{logMessage}");
            w.WriteLine("-------------------------------");
        }

        public static void DumpLog(StreamReader r)
        {
            string line;
            while ((line = r.ReadLine()) != null)
            {
                Console.WriteLine(line);
            }
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