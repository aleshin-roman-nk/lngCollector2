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
	}
}
