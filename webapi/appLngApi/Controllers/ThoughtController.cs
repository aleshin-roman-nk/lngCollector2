using Microsoft.AspNetCore.Mvc;
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
        [HttpGet("{nodeid}")]
        public IEnumerable<Thought> Get(int nodeid)
        {
            var thoughts = repo.Get(nodeid);

            return thoughts;
        }

        // POST api/<ThoughtController>
        [HttpPost]
        public Thought Create(int nodeId, [FromBody] Thought value)
        {
            return repo.Create(nodeId, value);
        }

        // PUT api/<ThoughtController>/5
        [HttpPut("{id}")]
        public void Update(int id, [FromBody] Thought value)
        {
            repo.Update(id, value);
        }

        // DELETE api/<ThoughtController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
