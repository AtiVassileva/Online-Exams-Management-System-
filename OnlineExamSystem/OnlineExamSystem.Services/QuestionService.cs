using Microsoft.EntityFrameworkCore;
using OnlineExamSystem.Data;
using OnlineExamSystem.Data.Models;

namespace OnlineExamSystem.Services
{
    public class QuestionService
    {
        private readonly OnlineExamSystemContext _dbContext;
        private readonly DbSet<Question> _questions;

        public QuestionService(OnlineExamSystemContext context)
        {
            _dbContext = context;
            _questions = context.Questions;
        }

        public async Task<Question> GetQuestionById(Guid id)
        {
            var question = await _questions.FirstOrDefaultAsync(e => e.Id == id);

            if (question == null)
            {
                throw new NullReferenceException("Question does not exist!");
            }

            return question;
        }

        public async Task<IEnumerable<Question>> GetAllQuestions()
        {
            return await _questions.ToListAsync();
        }

        public async Task<Guid> CreateQuestion(Question question)
        {
            await _dbContext.Questions.AddAsync(question);
            await _dbContext.SaveChangesAsync();
            return question.Id;
        }

        public async Task<bool> EditQuestion(Guid id, Question modifiedQuestion)
        {
            var question = await GetQuestionById(id);

            question.QuestionText = modifiedQuestion.QuestionText;
            question.CorrectAnswer = modifiedQuestion.CorrectAnswer;
            question.Points = modifiedQuestion.Points;

            _dbContext.Entry(question).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteQuestion(Guid id)
        {
            var question = await GetQuestionById(id);
            _dbContext.Questions.Remove(question);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}