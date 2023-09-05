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
			var opres = srv.GetAll();
			return processResult(opres.Success, opres);
		}

		[HttpPost]
		public IActionResult Create([FromBody] Language lng)
		{
			var opres = srv.Create(lng);
			return processResult(opres.Success, opres);
		}

		[HttpPut]
		public IActionResult Update([FromBody] Language lng)
		{
			var opres = srv.Create(lng);
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
