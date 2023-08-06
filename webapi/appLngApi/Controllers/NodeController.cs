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
    public class NodeController : ControllerBase
    {
        private readonly NodeService srv;

        public NodeController(NodeService srv) 
        {
            this.srv = srv;
        }

        // GET api/<NodeController>/5
        [HttpGet("{nodeid}/detail")]
        public IActionResult GetDetail(int nodeid)
        {
            var opres = srv.GetDetail(nodeid);

            return processResult(opres.Success, opres);
        }

        // POST api/<NodeController>
        [HttpPost]
        public IActionResult Create([FromBody] Node value)
        {
            var opres = srv.Create(value);
            return processResult(opres.Success, opres);
        }

        // PUT api/<NodeController>/5
        [HttpPut]
        public IActionResult Put([FromBody] Node value)
        {
            var opres = srv.Update(value);
            return processResult(opres.Success, opres);
        }

        // DELETE api/<NodeController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var opres = srv.Remove(id);
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
