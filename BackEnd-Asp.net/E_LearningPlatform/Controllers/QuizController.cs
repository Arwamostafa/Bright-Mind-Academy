using Domain.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service.Services.Contract;
using Service.Services.Implementation;

namespace lab1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuizController : ControllerBase
    {
        private IQuizService quizService;
        public QuizController(IQuizService _quizService)
        {
            quizService = _quizService;
        }

        [HttpGet("getall")]
        public IActionResult getallquiz()
        {
            List<quizdto> result = quizService.getallquizzes();
            if (result != null && result.Count > 0)
            {
                return Ok(result);
            }
            return NotFound("No quizzes found.");
        }
        [HttpPost]
        public IActionResult Add([FromBody] quizdto quiz)
        {
            var result = quizService.addquiz(quiz);
            if (result > 0) return Ok(result);
            if (result == -1) return Conflict("Quiz already exists.");
            return BadRequest();
        }
        [HttpGet("getById/{id}")]
        public IActionResult getquiz(int id)
        {

            quizdto result = quizService.getquiz(id);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest("no quiz with this id");
        }

        //[HttpPost("update")]
        [HttpPut("update/{id}")]
        public IActionResult Update(int id, [FromBody] quizdto quiz)
        {
            quiz.Id = id;
            var result = quizService.updatequiz(quiz);
            if (result == 1) return Ok();
            if (result == -1) return NotFound();
            return BadRequest();
        }

        //[HttpDelete("{id}")]
        //public IActionResult Delete(int id)
        //{
        //    var result = quizService.deletequiz(id);
        //    if (result == 1) return Ok();
        //    return NotFound();
        //}

        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            var result = quizService.deletequiz(id);
            if (result == 1) return Ok();
            return NotFound();
        }

        [HttpGet("bylesson/{lessonId}")]
        public IActionResult GetQuizByLessonId(int lessonId)
        {
            var quiz = quizService.GetQuizByLessonId(lessonId);
            if (quiz == null)
                return NotFound(new { message = "No quiz found for this lesson." });

            return Ok(quiz);
        }
    }

}