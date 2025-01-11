using Microsoft.AspNetCore.Mvc;
using OnlineExamSystem.API.Models;
using OnlineExamSystem.Data.Models;
using OnlineExamSystem.Services;

namespace OnlineExamSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsExamsController : ControllerBase
    {
        private readonly StudentExamService _studentExamService;
        private readonly UserService _userService;
        private readonly ExamService _examService;

        public StudentsExamsController(StudentExamService studentExamService, UserService userService, ExamService examService)
        {
            _studentExamService = studentExamService;
            _userService = userService;
            _examService = examService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Exam>>> GetExamsForStudent(Guid id)
        {
            try
            {
                var student = await _userService.GetUserById(id);
                var exams = await _studentExamService.GetExamsForStudent(student.Id);
                return Ok(exams);
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
        public async Task<ActionResult<IEnumerable<Exam>>> AssignStudentToExam([FromBody] StudentExamFormModel model)
        {
            try
            {
                var student = await _userService.GetUserById(model.StudentId);
                var exam = await _examService.GetExamById(model.ExamId);

                var result = await _studentExamService.AssignStudentToExam(student.Id, exam.Id);
                return Ok(result);
            }
            catch (NullReferenceException nre)
            {
                return NotFound(nre.Message);
            }
            catch (InvalidOperationException ioe)
            {
                return BadRequest(ioe.Message);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<IEnumerable<Exam>>> UpdateStudentExam(Guid id, [FromBody] StudentExam model)
        {
            try
            {
                await _userService.GetUserById(model.StudentId);
                await _examService.GetExamById(model.ExamId);

                var result = await _studentExamService.UpdateStudentExam(id, model);
                return Ok(result);
            }
            catch (NullReferenceException nre)
            {
                return NotFound(nre.Message);
            }
            catch (InvalidOperationException ioe)
            {
                return BadRequest(ioe.Message);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveStudentFromExam(StudentExamFormModel model)
        {
            try
            {
                var result = await _studentExamService.RemoveStudentFromExam(model.StudentId, model.ExamId);
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