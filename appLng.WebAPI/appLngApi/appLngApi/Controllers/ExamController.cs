using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services;
using Models.Location;
using Models.Exam.dto;
using Services.repo;

namespace appLngApi.Controllers
{
    [Route("api/exam")]
    [ApiController]
    public class ExamController : ControllerBase
    {
        private readonly ExamService service;

        private readonly ThoughtRepo threpo;
        private readonly ThExpressionRepo thexprepo;
        private readonly QuestionRepo questionRepo;

        public ExamController()
        {
            var f = new DbFactory(@"..\db\lngapp.sqlite");

            threpo = new ThoughtRepo(f);
            thexprepo = new ThExpressionRepo(f);
            questionRepo = new QuestionRepo(f);

            service = new ExamService(threpo, thexprepo, questionRepo);
        }

        [HttpPost("check")]
        public IActionResult Check(QuestSolution p)
        {
            try
            {
                return Ok(service.Check(p));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("questions")]
        public IActionResult GetQuestions(int nodeid)
        {
            try
            {
                return Ok(service.GetQuestions(nodeid));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
