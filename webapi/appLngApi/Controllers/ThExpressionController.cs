﻿using Microsoft.AspNetCore.Mvc;
using Models.Common;
using Models.Thought;
using Services;
using Services.repo;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace appLngApi.Controllers
{
    [Route("api/expression")]
    [ApiController]
    public class ThExpressionController : ControllerBase
    {
        private readonly IThExpressionRepo repo;

        public ThExpressionController(IThExpressionRepo rp)
        {
            repo = rp;
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
        public ThExpression Create(int thoughtId, [FromBody] ThExpression value)
        {
            return repo.Create(thoughtId, value);
        }

        // PUT api/<ThExpressionController>/5
        [HttpPatch("{id}")]
        public IActionResult Update(int id, [FromBody] UpdatePropertyDto o)
        {
            return Ok(repo.UpdateString(id, o.name, o.value));
        }

        // DELETE api/<ThExpressionController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var res = repo.Delete(id);

            if (res.success) return Ok(new { txt = res.message });

            return BadRequest(res.message);
        }
    }
}
