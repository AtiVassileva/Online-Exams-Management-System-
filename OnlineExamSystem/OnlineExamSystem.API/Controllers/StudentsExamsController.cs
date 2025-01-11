using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineExamSystem.Data;
using OnlineExamSystem.Data.Models;
using OnlineExamSystem.Services;

namespace OnlineExamSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsExamsController : ControllerBase
    {
        private readonly OnlineExamSystemContext _context;
        private readonly ExamService _examService;
        private readonly UserService _userService;

        public StudentsExamsController(OnlineExamSystemContext context, ExamService examService, UserService userService)
        {
            _context = context;
            _examService = examService;
            _userService = userService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Exam>>> GetExamsForStudent(Guid id)
        {
            try
            {
                var student = await _userService.GetUserById(id);
                var exams = await _examService.GetExamsForStudent(student.Id);
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


        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudentExam(Guid id, StudentExam studentExam)
        {
            if (id != studentExam.Id)
            {
                return BadRequest();
            }

            _context.Entry(studentExam).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExamExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<StudentExam>> PostStudentExam(StudentExam studentExam)
        {
            _context.StudentsExams.Add(studentExam);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStudentExam", new { id = studentExam.Id }, studentExam);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudentExam(Guid id)
        {
            var studentExam = await _context.StudentsExams.FindAsync(id);
            if (studentExam == null)
            {
                return NotFound();
            }

            _context.StudentsExams.Remove(studentExam);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StudentExamExists(Guid id)
        {
            return _context.StudentsExams.Any(e => e.Id == id);
        }
    }
}
