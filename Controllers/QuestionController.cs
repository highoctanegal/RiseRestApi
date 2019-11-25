using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RiseRestApi.Models;
using RiseRestApi.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RiseRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : BaseApiController
    {
        public QuestionController(RiseContext context) : base (context) { }
               
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Question>>> GetQuestion()
        {
            return await _context.Question.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IModel>> GetQuestion(int id)
        {
            return await Get(id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuestion(int id, Question question)
        {
            return await Put(id, question);
        }

        [HttpPost]
        public async Task<ActionResult<IModel>> PostQuestion(Question question)
        {
            return await Post(question);
        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult<IModel>> DeleteQuestion(int id)
        {
            return await Delete(id);
        }

        public override bool Exists(int id)
        {
            return _context.Question.Any(e => e.QuestionId == id);
        }

        public override async Task<IModel> FindAsync(int id)
        {
            return await _context.Question.FindAsync(id) as IModel;
        }
    }
}
