using Microsoft.EntityFrameworkCore;
using OnlineExamSystem.Data.Models;
using OnlineExamSystem.Data;

namespace OnlineExamSystem.Services
{
    public class StudentExamService
    {
        private readonly OnlineExamSystemContext _dbContext;
        private readonly DbSet<StudentExam> _studentExams;

        public StudentExamService(OnlineExamSystemContext context)
        {
            _dbContext = context;
            _studentExams = context.StudentsExams;
        }

        public async Task<IEnumerable<Exam>> GetExamsForStudent(Guid studentId)
        {
            var examsForStudentIds = await _studentExams
                .Include(se => se.Student)
                .Where(se => se.StudentId == studentId)
                .Select(se => se.ExamId)
                .ToListAsync();

            var exams = await _dbContext.Exams
                .Include(e => e.Author)
                .Include(e => e.Status)
                .Where(e => examsForStudentIds.Contains(e.Id))
                .ToListAsync();

            return exams;
        }

        public async Task<bool> AssignStudentToExam(Guid studentId, Guid examId)
        {
            var existingStudentExam = await FindStudentExam(studentId, examId);

            if (existingStudentExam != null)
            {
                throw new InvalidOperationException("Student is already assigned to this exam!");
            }

            var studentExam = new StudentExam
            {
                StudentId = studentId,
                ExamId = examId
            };

            await _studentExams.AddAsync(studentExam);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> RemoveStudentFromExam(Guid studentId, Guid examId)
        {
            var studentExam = await FindStudentExam(studentId, examId);

            if (studentExam == null)
            {
                throw new NullReferenceException("Student is not assigned to exam!");
            }

            _studentExams.Remove(studentExam);
            await  _dbContext.SaveChangesAsync();

            return true;
        }

        private async Task<StudentExam?> FindStudentExam(Guid studentId, Guid examId)
        {
            return await _studentExams.FirstOrDefaultAsync(se => se.StudentId == studentId && se.ExamId == examId);
        }
    }
}
