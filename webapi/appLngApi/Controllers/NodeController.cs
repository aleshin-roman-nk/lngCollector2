using Microsoft.AspNetCore.Mvc;
using Models.Location;
using Models.Location.dto;
using Services.repo;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace appLngApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NodeController : ControllerBase
    {
        private readonly INodeRepo repo;

        public NodeController(INodeRepo rp) 
        {
            repo = rp;
        }

        // GET api/<NodeController>/5
        [HttpGet("{nodeid}/detail")]
        public NodeDetail GetDetail(int nodeid)
        {
            return repo.GetDetail(nodeid);
        }

        // POST api/<NodeController>
        [HttpPost]
        public Node Create(int terrId, [FromBody] Node value)
        {
            return repo.Create(terrId, value);
        }

        // PUT api/<NodeController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Node value)
        {
            repo.Update(id, value);
        }

        // DELETE api/<NodeController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
