using Microsoft.AspNetCore.Mvc;
using ThoughtzLand.Core.Models.Common;
using ThoughtzLand.Core.Models.Thoughts;
using ThoughtzLand.Core.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ThoughtzLand.Api.Controllers
{
    [Route("api/expression")]
    [ApiController]
    public class ThExpressionController : ControllerBase
    {
        private readonly ThExpressionService srv;
		private readonly SRBoxService srbSrv;

		public ThExpressionController(ThExpressionService s, SRBoxService srbSrv)
        {
            this.srv = s;
			this.srbSrv = srbSrv;
		}

        [HttpGet("one/{id}")]
        public IActionResult Get(int id)
        {
            var opres = srv.GetById(id);

            return processResult(opres.Success, opres);
        }

        [HttpGet("{thoughtid}")]
        public IActionResult GetByThoughtId(int thoughtid)
        {
            var opres = srv.GetByThoughtId(thoughtid);

            return processResult(opres.Success, opres);
        }

        [HttpPost]
        public IActionResult Create([FromBody] ThExpression value)
        {
            var opres = srv.Add(value);

            // Плохо
            if (opres.Success)
                srbSrv.CreateCard(opres.Content.thoughtId);

			return processResult(opres.Success, opres);
        }

        [HttpPatch]
        public IActionResult UpdateProperty([FromBody] UpdateStringPropertyDto o)
        {
            var opres = srv.UpdateString(o.id, o.name, o.value);

            return processResult(opres.Success, opres);
        }

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
