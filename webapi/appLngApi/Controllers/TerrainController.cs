using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ThoughtzLand.Core.Models.Common;
using ThoughtzLand.Core.Models.Location;
using ThoughtzLand.Core.Models.Location.dto;
using ThoughtzLand.Core.Services;

namespace ThoughtzLand.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class TerrainController : ControllerBase
	{
		private readonly TerrainService terrainSrv;
		//private readonly NodeService nodeSrv;

		public TerrainController(TerrainService terrainSrv/*, NodeService nodeSrv*/)
		{
			this.terrainSrv = terrainSrv;
			//this.nodeSrv = nodeSrv;
		}

		[HttpGet]
		public IActionResult Get()
		{
			return Ok(terrainSrv.GetAllTerrainTitles());
		}

		[HttpGet("{id}")]
		public IActionResult Get(int id)
		{
			return Ok(terrainSrv.GetTerrainDetail(id));
		}

		// 03-02-2024
		// возможно потребуется подгрузка в будущем, если не грузить все объекты сцены
		// вместо этого типа пагинации, если нод слишком много
		//[HttpGet("{id}/nodes")]
		//public IActionResult GetNodes(int id)
		//{
		//	return Ok(nodeSrv.GetByTerrainId(id));
		//}

		[HttpPost]
		public IActionResult Post([FromBody] CreateTerrainDto t)
		{
			return Ok(terrainSrv.Create(t));
		}

		[HttpPatch("update-string")]
		public IActionResult updateString([FromBody] UpdatePropertyDto<string> dto)
		{
			terrainSrv.UpdateProperty(dto.id, dto.name, dto.value);
			return Ok();
		}

		[HttpPut]
		public IActionResult update([FromBody] UpdateTerrainDto dto)
		{
			terrainSrv.Update(dto);
			return Ok();
		}

		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			terrainSrv.Remove(id);
			return Ok();
		}
	}
}
