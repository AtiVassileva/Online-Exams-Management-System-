using Microsoft.EntityFrameworkCore;
using OnlineExamSystem.Data;
using OnlineExamSystem.Data.Models;

namespace OnlineExamSystem.Services
{
    public class ExamService
    {
        private readonly OnlineExamSystemContext _dbContext;
        private readonly DbSet<Exam> _exams;

        public ExamService(OnlineExamSystemContext context)
        {
            _dbContext = context;
            _exams = context.Exams;
        }

        public async Task<Exam> GetExamById(Guid id)
        {
            var exam = await _exams.FirstOrDefaultAsync(e => e.Id == id);

            if (exam == null)
            {
                throw new NullReferenceException("Exam does not exist!");
            }

            return exam;
        }

        public async Task<IEnumerable<Exam>> GetAllExams()
        {
            return await _exams.ToListAsync();
        }

        public async Task<Guid> CreateExam(Exam exam)
        {
            await _dbContext.Exams.AddAsync(exam);
            await _dbContext.SaveChangesAsync();
            return exam.Id;
        }

        public async Task<IEnumerable<Exam>> GetExamsForStudent(Guid studentId)
        {
            var examsForStudentIds = await _dbContext.StudentsExams
                .Where(se => se.StudentId == studentId)
                .Select(se => se.ExamId)
                .ToListAsync();

            var exams = await _exams
                .Include(e => e.Author)
                .Include(e => e.Status)
                .Where(e => examsForStudentIds.Contains(e.Id))
                .ToListAsync();

            return exams;
        }

        public async Task<bool> EditExam(Guid id, Exam modifiedExam)
        {
            var exam = await GetExamById(id);

            exam.Title = modifiedExam.Title;
            exam.Description = modifiedExam.Description;
            exam.StartTime = modifiedExam.StartTime;
            exam.Duration = modifiedExam.Duration;
            exam.StatusId = modifiedExam.StatusId;

            _dbContext.Entry(exam).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteExam(Guid id)
        {
            var exam = await GetExamById(id);
            _dbContext.Exams.Remove(exam);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}