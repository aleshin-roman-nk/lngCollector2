using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Location;
using Services.repo;

namespace appLngApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TerrainController : ControllerBase
    {
        private readonly ITerrainRepo _repo;
        private readonly INodeRepo nrepo;

        public TerrainController(ITerrainRepo repo, INodeRepo nrepo)
        {
            _repo = repo;
            this.nrepo = nrepo;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_repo.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var res = _repo.Get(id);

            if (res == null)
                return StatusCode(500, $"Terrain id = {id} is not found");
            else
                return Ok(res);
        }

        [HttpGet("{id}/nodes")]
        public IActionResult GetNodes(int id)
        {
            var res = nrepo.GetAllOf(id);

            if (res == null)
                return StatusCode(500, $"Terrain id = {id} is not found");
            else
                return Ok(res);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Terrain t)
        {
            var res = _repo.Create(t);

            if (t.id == 0) return StatusCode(500, $"Something went wrong with creating a new terrain");
            else return Ok(t);
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult updateTerrain(int id, Terrain ter)
        {
            try
            {
                return Ok(_repo.Update(id, ter));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _repo.Delete(id);

            return Ok(new {text = "Deleted"});
        }
    }
}
