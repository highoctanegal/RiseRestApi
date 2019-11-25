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
    public class SurveyController : BaseApiController
    {
        public SurveyController(RiseContext context) : base(context) { }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Survey>>> GetSurvey()
        {
            return await _context.Survey.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IModel>> GetSurvey(int id)
        {
            return await Get(id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutSurvey(int id, Survey survey)
        {
            return await Put(id, survey);
        }

        [HttpPost]
        public async Task<ActionResult<IModel>> PostSurvey(Survey survey)
        {
            return await Post(survey);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<IModel>> DeleteSurvey(int id)
        {
            return await Delete(id);
        }

        public override bool Exists(int id)
        {
            return _context.Survey.Any(e => e.SurveyId == id);
        }

        public override async Task<IModel> FindAsync(int id)
        {
            return await _context.Survey.FindAsync(id) as IModel;
        }
    }
}
