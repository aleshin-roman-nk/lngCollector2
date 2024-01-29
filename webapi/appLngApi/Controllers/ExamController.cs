using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ThoughtzLand.Core.Models.Exam.dto;
using ThoughtzLand.Core.Services.FlashCards;

namespace ThoughtzLand.Api.Controllers
{
    [Route("api/flashcard")]
    [ApiController]
    [Authorize]
    public class ExamController : ControllerBase
    {
        private readonly FlashCardExamService srv;

        public ExamController(FlashCardExamService s)
        {
            this.srv = s;
        }

        [HttpPost("check")]
        public IActionResult Check(CardSolution p)
        {
            return Ok(srv.Check(p));
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

  //      [HttpGet("node/{id}/cards")]
  //      public IActionResult GetCards(int id, DateTime d)
  //      {
  //          var opres = srv.GetCards(id, d);

		//	return processResult(opres.Success, opres);
		//}

  //      [HttpGet("card/{id}")]
  //      public IActionResult GetCard(int id)
  //      {
		//	var opres = srv.GetCard(id);

		//	return processResult(opres.Success, opres);
		//}
    }
}
