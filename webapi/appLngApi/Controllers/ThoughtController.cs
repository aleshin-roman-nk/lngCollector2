using Microsoft.AspNetCore.Mvc;
using ThoughtzLand.Core.Models.Common;
using ThoughtzLand.Core.Models.Thoughts;
using ThoughtzLand.Core.Repos;
using ThoughtzLand.Core.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ThoughtzLand.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThoughtController : ControllerBase
    {
        private readonly ThoughtService srv;

        public ThoughtController(ThoughtService srv)
        {
            this.srv = srv;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var opres = srv.GetById(id);

            return processResult(opres.Success, opres);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Thought value)
        {
            var opres = srv.Add(value);

            return processResult(opres.Success, opres);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var opres = srv.Remove(id);

            return processResult(opres.Success, opres);
        }

        [HttpPatch]
        public IActionResult UpdateProperty([FromBody] UpdateStringPropertyDto prop)
        {
            var opres = srv.UpdateString(prop.id, prop.name, prop.value);

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
