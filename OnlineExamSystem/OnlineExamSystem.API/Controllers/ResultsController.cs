using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineExamSystem.Data;
using OnlineExamSystem.Data.Models;
using OnlineExamSystem.Services;

namespace OnlineExamSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResultsController : ControllerBase
    {
        private readonly ResultService _resultService;

        public ResultsController(ResultService resultService)
        {
            _resultService = resultService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Result>>> GetResults()
        {
            return Ok(await _resultService.GetAllResults());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Result>> GetResult(Guid id)
        {
            try
            {
                var result = await _resultService.GetResultById(id);
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

        [HttpPost]
        public async Task<ActionResult<Result>> PostResult([FromBody] Result result)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                var id = await _resultService.CreateResult(result);
                return Ok(id);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutResult(Guid id, Result result)
        {
            try
            {
                var isSuccessful = await _resultService.EditResult(id, result);
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
        public async Task<IActionResult> DeleteResult(Guid id)
        {
            try
            {
                var result = await _resultService.DeleteResult(id);
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