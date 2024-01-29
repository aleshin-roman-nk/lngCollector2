using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ThoughtzLand.Core.Models.Location;
using ThoughtzLand.Core.Models.Location.dto;
using ThoughtzLand.Core.Repos;
using ThoughtzLand.Core.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ThoughtzLand.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class NodeController : ControllerBase
	{
		private readonly NodeService srv;

		public NodeController(NodeService srv)
		{
			this.srv = srv;
		}

		[HttpGet("{nodeid}/detail")]
		public IActionResult GetDetail(int nodeid)
		{
			return Ok(srv.GetDetail(nodeid));
		}

		[HttpPost]
		public IActionResult Create([FromBody] Node value)
		{
			return Ok(srv.Create(value));
		}

		[HttpPut]
		public IActionResult Put([FromBody] UpdateNodeNameAndDescriptionDto value)
		{
			srv.Update(value);
			return Ok();
		}

		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			srv.Remove(id);
			return Ok();
		}
	}
}
