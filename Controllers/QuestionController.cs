using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RiseRestApi.Models;
using RiseRestApi.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;

namespace RiseRestApi.Controllers
{
    [EnableCors("AllowAll")]
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : BaseApiController<Question>
    {
        public QuestionController(RiseContext context) : base(context) { }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Question>>> GetQuestion()
        {
            return await _context.Question.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Question>> GetQuestion(int id)
        {
            return await Get(id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuestion(int id, Question question)
        {
            return await Put(id, question);
        }

        [HttpPost]
        public async Task<ActionResult<Question>> PostQuestion(Question question)
        {
            return await Post(question);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Question>> DeleteQuestion(int id)
        {
            return await Delete(id);
        }

        protected override bool Exists(int id)
        {
            return _context.Question.Any(e => e.QuestionId == id);
        }

        protected override async Task<Question> FindAsync(int id)
        {
            return await _context.Question.FindAsync(id) as Question;
        }
    }
}
