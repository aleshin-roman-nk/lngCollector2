using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services.repo;

namespace appLngApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TerrainController : ControllerBase
    {
        private readonly TerrainRepo repo;

        public TerrainController()
        {
            repo = new TerrainRepo(new Services.DbFactory(@"..\db\lngapp.sqlite"));
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(repo.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var res = repo.Get(id);

            if (res == null)
                return StatusCode(500, $"Terrain id = {id} is not found");
            else
                return Ok(res);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Terrain t)
        {
            var res = repo.Create(t);

            if (t.id == 0) return StatusCode(500, $"Something went wrong with creating a new terrain");
            else return Ok(t);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            repo.Delete(id);

            return Ok(new {text = "Deleted"});
        }
    }
}
