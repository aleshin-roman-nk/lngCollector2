using Microsoft.AspNetCore.Mvc;
using Models.Thought;
using Services.repo;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace appLngApi.Controllers
{
    [Route("api/expression")]
    [ApiController]
    public class ThExpressionController : ControllerBase
    {
        private readonly ThExpressionRepo repo;

        public ThExpressionController()
        {
            repo = new ThExpressionRepo(new Services.DbFactory(@"..\db\lngapp.sqlite"));
        }


        // GET: api/<ThExpressionController>
        [HttpGet("one/{id}")]
        public ThExpression Get(int id)
        {
            return repo.GetOne(id);
        }

        // GET api/<ThExpressionController>/5
        [HttpGet("{thoughtid}")]
        public IEnumerable<ThExpression> GetAllOf(int thoughtid)
        {
            return repo.GetAllOf(thoughtid);
        }

        // POST api/<ThExpressionController>
        [HttpPost]
        public void Create(int thoughtId, [FromBody] ThExpression value)
        {
            repo.Create(thoughtId, value);
        }

        // PUT api/<ThExpressionController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ThExpressionController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
