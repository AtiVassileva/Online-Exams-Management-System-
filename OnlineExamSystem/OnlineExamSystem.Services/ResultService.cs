using Microsoft.EntityFrameworkCore;
using OnlineExamSystem.Data.Models;
using OnlineExamSystem.Data;

namespace OnlineExamSystem.Services
{
    public class ResultService
    {
        private readonly OnlineExamSystemContext _dbContext;
        private readonly DbSet<Result> _results;

        public ResultService(OnlineExamSystemContext context)
        {
            _dbContext = context;
            _results = context.Results;
        }

        public async Task<Result> GetResultById(Guid id)
        {
            var result = await _results.FirstOrDefaultAsync(e => e.Id == id);

            if (result == null)
            {
                throw new NullReferenceException("Result does not exist!");
            }

            return result;
        }

        public async Task<IEnumerable<Result>> GetAllResults()
        {
            return await _results.ToListAsync();
        }

        public async Task<Guid> CreateResult(Result result)
        {
            await _dbContext.Results.AddAsync(result);
            await _dbContext.SaveChangesAsync();
            return result.Id;
        }

        public async Task<bool> EditResult(Guid id, Result modifiedResult)
        {
            var result = await GetResultById(id);

            result.Score = modifiedResult.Score;

            _dbContext.Entry(result).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteResult(Guid id)
        {
            var result = await GetResultById(id);
            _dbContext.Results.Remove(result);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}