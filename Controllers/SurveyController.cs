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
    public class SurveyController : BaseApiController<Survey>
    {
        public SurveyController(RiseContext context) : base(context) { }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Survey>>> GetSurvey()
        {
            return await _context.Survey.Where(s => !s.IsRemoved).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Survey>> GetSurvey(int id)
        {
            return await Get(id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutSurvey(int id, Survey survey)
        {
            return await Put(id, survey);
        }

        [HttpPost]
        public async Task<ActionResult<Survey>> PostSurvey(Survey survey)
        {
            return await Post(survey);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Survey>> DeleteSurvey(int id)
        {
            var survey = _context.Survey.Where(s => s.SurveyId == id).FirstOrDefault();
            if (survey == null)
            {
                return new NotFoundResult();
            }
            survey.IsRemoved = true;
            await Put(id, survey);
            return new JsonResult(survey);
        }

        protected override bool Exists(int id)
        {
            return _context.Survey.Any(e => e.SurveyId == id);
        }

        protected override async Task<Survey> FindAsync(int id)
        {
            return await _context.Survey.FindAsync(id) as Survey;
        }
    }
}
