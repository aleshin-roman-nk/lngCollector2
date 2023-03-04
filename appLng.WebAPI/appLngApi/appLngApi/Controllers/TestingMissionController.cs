using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.dto;
using Models;
using Services;

namespace appLngApi.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/tst-miss")]
    [ApiController]
    public class TestingMissionController : ControllerBase
    {
        LexemTestingMissionService service = new LexemTestingMissionService(new DbFactory(@"..\db\lngapp.sqlite"));

        [HttpPost("check")]
        //public SolutionCheckResultFrame CheckTestPoint(QuestSolution p)
        public IActionResult CheckTestPoint(QuestSolution p)
        {
            try
            {
                return Ok(service.CheckSolutionAndNext(p));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("starttest")]
        public IActionResult StartTest(int id)
        {
            try
            {
                service.StartSession(new TestingMission { id = id });
                return Ok("Session has successfully started");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("currentquestion")]
        public IActionResult GetCurrentQuestion(int id)
        {
            try
            {
                return Ok(service.CurrentQuestion(new TestingMission { id = id }));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
