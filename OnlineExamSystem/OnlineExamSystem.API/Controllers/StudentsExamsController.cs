using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineExamSystem.Data;
using OnlineExamSystem.Data.Models;

namespace OnlineExamSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsExamsController : ControllerBase
    {
        private readonly OnlineExamSystemContext _context;

        public StudentsExamsController(OnlineExamSystemContext context)
        {
            _context = context;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentExam>>> GetStudentsExams()
        {
            return await _context.StudentsExams.ToListAsync();
        }
        
        //[HttpGet("{id}")]
        //public async Task<ActionResult<StudentExam>> GetStudentExam(Guid id)
        //{
        //    var studentExam = await _context.StudentsExams.FindAsync(id);

        //    if (studentExam == null)
        //    {
        //        return NotFound();
        //    }

        //    return studentExam;
        //}

        [HttpGet("{id}")]
        public async Task<ActionResult<List<Exam>>> GetExamsForStudent(Guid id)
        {
            var student = await _context.Users.FirstOrDefaultAsync(s => s.Id == id);

            if (student == null)
            {
                return NotFound("Student does not exist!");
            }

            var examsForStudentIds = _context.StudentsExams
                .Where(se => se.StudentId == id)
                .Select(se => se.ExamId)
                .ToList();

            var exams = _context.Exams
                    .Include(e => e.Status)
                    .Where(e => examsForStudentIds.Contains(e.Id))
                    .ToList();

            return exams;
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
