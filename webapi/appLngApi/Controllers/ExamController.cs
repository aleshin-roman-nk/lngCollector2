using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ThoughtzLand.Core.Models.Exam.dto;
using ThoughtzLand.Core.Services;

namespace ThoughtzLand.Api.Controllers
{
    [Route("api/exam")]
    [ApiController]
    public class ExamController : ControllerBase
    {
        private readonly SRBoxService srv;

        public ExamController(SRBoxService s)
        {
            this.srv = s;
        }

        [HttpPost("check")]
        public IActionResult Check(CardSolution p)
        {
            var opres = srv.Check(p);

            return processResult(opres.Success, opres);
        }

        //[HttpGet("questions")]
        //public IActionResult GetQuestions(int nodeid)
        //{
        //    try
        //    {
        //        return Ok(srv.GetQuestions(nodeid));
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, ex.Message);
        //    }
        //}

        [HttpGet("node/{id}/cards")]
        public IActionResult GetCards(int id, DateTime d)
        {
            var opres = srv.GetCards(id, d);

			return processResult(opres.Success, opres);
		}

        [HttpGet("card/{id}")]
        public IActionResult GetCard(int id)
        {
			var opres = srv.GetCard(id);

			return processResult(opres.Success, opres);
		}

        private IActionResult processResult(bool ok, object o)
        {
            if (ok)
                return Ok(o);
            else
                return BadRequest(o);
        }
    }
}
