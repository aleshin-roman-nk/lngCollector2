using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ThoughtzLand.Core.Models.Location;
using ThoughtzLand.Core.Services;

namespace ThoughtzLand.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TerrainController : ControllerBase
    {
        private readonly TerrainService terrainSrv;
        private readonly NodeService nodeSrv;

        public TerrainController(TerrainService terrainSrv, NodeService nodeSrv)
        {
            this.terrainSrv = terrainSrv;
            this.nodeSrv = nodeSrv;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var opres = terrainSrv.GetAll();

            return processResult(opres.Success, opres);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var opres = terrainSrv.GetById(id);

            return processResult(opres.Success, opres);
        }

        [HttpGet("{id}/nodes")]
        public IActionResult GetNodes(int id)
        {
            var opres = nodeSrv.GetByTerrainId(id);

            return processResult(opres.Success, opres);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Terrain t)
        {
            var opres = terrainSrv.Create(t);

            return processResult(opres.Success, opres);
        }

        [HttpPut]
        public IActionResult updateTerrain(Terrain ter)
        {
            var opres = terrainSrv.Update(ter);

            return processResult(opres.Success, opres);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var opres = terrainSrv.Remove(id);

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
