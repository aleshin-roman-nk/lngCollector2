using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ThoughtzLand.Core.Models.Common;
using ThoughtzLand.Core.Services;

namespace ThoughtzLand.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class LanguagesController : ControllerBase
	{
		private readonly LanguagesService srv;

		public LanguagesController(LanguagesService srv)
		{
			this.srv = srv;
		}

		[HttpGet]
		public IActionResult GetAll()
		{
            return Ok(srv.GetAll());
		}

		[HttpPost]
		public IActionResult Create([FromBody] Language lng)
		{
			return Ok(srv.Create(lng));
		}

		[HttpPut]
		public IActionResult Update([FromBody] Language lng)
		{
			return Ok(srv.Update(lng));
		}
	}
}
