using Microsoft.AspNetCore.Mvc;
using Models.Common;
using Models.Location;
using Models.Thought;
using Services.repo;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace appLngApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThoughtController : ControllerBase
    {
        private readonly IThoughtRepo repo;

        public ThoughtController(IThoughtRepo rp)
        {
            repo = rp;
        }

        // GET api/<ThoughtController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var opres = repo.Get(id);

            if(opres.Success)
                return Ok(opres);
            else 
                return BadRequest(opres.Message);
        }

        // POST api/<ThoughtController>
        [HttpPost]
        public IActionResult Create(int nodeId, [FromBody] Thought value)
        {
            var opres = repo.Create(nodeId, value);
            
            if (opres.Success)
                return Ok(opres);
            else
                return BadRequest(opres.Message);
        }

        // PUT api/<ThoughtController>/5
        //[HttpPut("{id}")]
        //public void Update(int id, [FromBody] Thought value)
        //{
        //    repo.Update(id, value);
        //}

        // DELETE api/<ThoughtController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        [HttpPatch("{id}")]
        public IActionResult UpdateProperty(int id, [FromBody] UpdatePropertyDto prop)
        {
            var opres = repo.UpdateString(id, prop.name, prop.value);

            if (opres.Success)
                return Ok(opres);
            else
                return BadRequest(opres.Message);
        }
    }
}
