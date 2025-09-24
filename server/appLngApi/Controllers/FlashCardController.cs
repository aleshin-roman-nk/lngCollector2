using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ThoughtzLand.Core.Models.Common;
using ThoughtzLand.Core.Models.Exam.dto;
using ThoughtzLand.Core.Models.Location;
using ThoughtzLand.Core.Services.FlashCards;

namespace ThoughtzLand.Api.Controllers
{
    [Route("api/flashcard")]
	[ApiController]
	[Authorize]
	public class FlashCardController : ControllerBase
	{
		private readonly FlashCardService service;

		public FlashCardController(FlashCardService service) 
		{
			this.service = service;
		}

		//[HttpGet("ofnode")]
		//public IActionResult Get(int nodeid, DateTime date)
		//{
		//	return Ok(service.Get(nodeid, date));
		//}

		[HttpGet("{id}")]
		public IActionResult Get(int id)
		{
			return Ok(service.GetSingle(id));
		}

		//[HttpPatch("answer/update-string")]
		//public IActionResult CardAnswerUpdateProperty([FromBody] UpdatePropertyDto<string> prop)
		//{
		//	service.CardAnswerUpdateProperty(prop.id, prop.name, prop.value);
		//	return Ok();
		//}

		//[HttpPatch("update-string")]
		//public IActionResult CardUpdateProperty([FromBody] UpdatePropertyDto<string> prop)
		//{
		//	service.CardUpdateProperty(prop.id, prop.name, prop.value);
		//	return Ok();
		//}

		[HttpPost("answer")]
		public IActionResult Add([FromBody] CreateFlashCardAnswerDto dto)
		{
			return Ok(service.AddAnswer(dto));
		}

		[HttpPost]
		public IActionResult Add([FromBody] CreateFlashCardDto dto) 
		{
			return Ok(service.Add(dto));
		}

		[HttpPut]
		public IActionResult Update([FromBody] UpdateFlashCardDto dto)
		{
			return Ok(service.UpdateFlashCard(dto));
		}

		[HttpPut("answer")]
		public IActionResult UpdateCardAnswer([FromBody] UpdateCardAnswerDto dto)
		{
			service.UpdateCardAnswer(dto);
			return Ok();
		}

		[HttpDelete("answer/{id}")]
		public IActionResult Delete(int id)
		{
			service.DeleteCardAnswer(id);
			return Ok();
		}

		[HttpGet("playing/ofnode/{nodeId}")]
		//public IActionResult GetPlayingCards(int nodeId, DateTime date)
		public IActionResult GetPlayingCards(int nodeId)
		{
			//return Ok(service.GetPlayingCards(nodeId, date));
			return Ok(service.GetPlayingCards(nodeId));
		}
	}
}
