using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ThoughtzLand.Core.Models.Location.dto;
using ThoughtzLand.Core.Services;

namespace ThoughtzLand.Api.Controllers
{
	[Route("api/research-text")]
	[ApiController]
	[Authorize]
	public class ResearchTextController : ControllerBase
	{
		private readonly ResearchTextService service;

		public ResearchTextController(ResearchTextService service)
		{
			this.service = service;
		}

		[HttpPost]
		public IActionResult Create([FromBody] CreateResearchTextDto dto)
		{
			var res = service.Create(dto);
			return Ok(res);
		}

		[HttpPut]
		public IActionResult Update([FromBody] UpdateResearchTextDto dto)
		{
			service.Update(dto);
			return Ok();
		}

		[HttpDelete("{textId}")]
		public IActionResult Delete(int textId)
		{
			service.Remove(textId);
			return Ok();
		}
	}
}
