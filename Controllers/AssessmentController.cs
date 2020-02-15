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
    public class AssessmentController : BaseApiController<Assessment>
    {
        public AssessmentController(RiseContext context) : base(context) { }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Assessment>>> GetAssessment()
        {
            return await _context.Assessment.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Assessment>> GetAssessment(int id)
        {
            return await Get(id);
        }

        [HttpGet("{id}/detail")]
        public async Task<ActionResult<AssessmentDetail>> GetDetail(int id)
        {
            if (!Exists(id))
            {
                return NotFound();
            }

            var model = await _context.AssessmentDetail.FromSqlRaw("EXEC spAssessmentDetail @AssessmentId={0}", id).ToListAsync();

            if (model == null)
            {
                return NotFound();
            }

            return model.First();
        }

        [HttpGet("person/{personId}")]
        public async Task<ActionResult<IEnumerable<AssessmentDetail>>> GetByPerson(int personId)
        {
            return await _context.AssessmentDetail.FromSqlRaw("EXEC spAssessmentDetail @PersonId={0}", personId).ToListAsync();
        }

        [HttpGet("person/{personId}/draft/{voicePersonId}")]
        public async Task<ActionResult<PersonAssessmentDetail>> GetDraft(int personId, int voicePersonId)
        {
            var list = await _context.PersonAssessmentDetail
                .FromSqlRaw("EXEC spPersonAssessmentDraft @PersonId={0}, @VoicePersonId={1}", personId, voicePersonId)
                .ToListAsync();

            return list.FirstOrDefault();
        }

        [HttpGet("person/{personId}/detail")]
        public async Task<ActionResult<IEnumerable<PersonAssessmentDetail>>> GetDetailByPerson(int personId)
        {
            return await _context.PersonAssessmentDetail.FromSqlRaw("EXEC spPersonAssessmentDetail {0}", personId).ToListAsync();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAssessment(int id, Assessment assessment)
        {
            return await Put(id, assessment);
        }

        [HttpPost]
        public async Task<ActionResult<Assessment>> PostAssessment(Assessment assessment)
        {
            return await Post(assessment);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Assessment>> DeleteAssessment(int id)
        {
            return await Delete(id);

        }

        protected override bool Exists(int id)
        {
            return _context.Assessment.Any(e => e.AssessmentId == id);
        }

        protected override async Task<Assessment> FindAsync(int id)
        {
            return await _context.Assessment.FindAsync(id) as Assessment;
        }
    }
}
