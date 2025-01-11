using Microsoft.AspNetCore.Mvc;
using OnlineExamSystem.Data.Models;
using OnlineExamSystem.Services;

namespace OnlineExamSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        private readonly QuestionService _questionService;

        public QuestionsController(QuestionService questionService)
        {
            _questionService = questionService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Question>>> GetQuestions()
        {
            return Ok(await _questionService.GetAllQuestions());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Question>> GetQuestion(Guid id)
        {
            try
            {
                var question = await _questionService.GetQuestionById(id);
                return Ok(question);
            }
            catch (NullReferenceException nre)
            {
                return NotFound(nre.Message);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Question>> PostQuestion([FromBody] Question question)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                var id = await _questionService.CreateQuestion(question);
                return Ok(id);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuestion(Guid id, Question question)
        {
            try
            {
                var isSuccessful = await _questionService.EditQuestion(id, question);
                return Ok(isSuccessful);
            }
            catch (NullReferenceException nre)
            {
                return NotFound(nre.Message);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuestion(Guid id)
        {
            try
            {
                var result = await _questionService.DeleteQuestion(id);
                return Ok(result);
            }
            catch (NullReferenceException nre)
            {
                return NotFound(nre.Message);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}