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
    public class SurveyQuestionController : BaseApiController<SurveyQuestion>
    {
        public SurveyQuestionController(RiseContext context) : base(context) { }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SurveyQuestion>>> GetSurveyQuestion()
        {
            return await _context.SurveyQuestion.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SurveyQuestion>> GetSurveyQuestion(int id)
        {
            return await Get(id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutSurveyQuestion(int id, SurveyQuestion surveyQuestion)
        {
            return await Put(id, surveyQuestion);
        }

        [HttpPost]
        public async Task<ActionResult<SurveyQuestion>> PostSurveyQuestion(SurveyQuestion surveyQuestion)
        {
            return await Post(surveyQuestion);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<SurveyQuestion>> DeleteSurveyQuestion(int id)
        {
            return await Delete(id);
        }

        protected override bool Exists(int id)
        {
            return _context.SurveyQuestion.Any(e => e.SurveyQuestionId == id);
        }

        protected override async Task<SurveyQuestion> FindAsync(int id)
        {
            return await _context.SurveyQuestion.FindAsync(id) as SurveyQuestion;
        }

    }
}
