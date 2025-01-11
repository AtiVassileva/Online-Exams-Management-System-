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

        public async Task<StudentExam> GetStudentExamById(Guid id)
        {
            var studentExam = await _studentExams.FirstOrDefaultAsync(se => se.Id == id);

            if (studentExam == null)
            {
                throw new NullReferenceException("Student exam does not exist!");
            }

            return studentExam;
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
                ExamId = examId,
                StatusId = Guid.Parse("C289614A-4E41-4A7D-9698-470BAFED4A5B"),
                Result = 0m,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await _studentExams.AddAsync(studentExam);
            await _dbContext.SaveChangesAsync();

            return true;
        }
        
        public async Task<bool> UpdateStudentExam(Guid id, StudentExam modifiedStudentExam)
        {
            var studentExam = await GetStudentExamById(id);

            studentExam.StatusId = modifiedStudentExam.StatusId;
            studentExam.Result = modifiedStudentExam.Result;
            studentExam.UpdatedAt = DateTime.UtcNow;
            
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
