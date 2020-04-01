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
    public class AssessmentController //: BaseApiController<Assessment>
    {
        private static IAssessmentRepository _assessmentRepository;
        //public AssessmentController(RiseContext context) : base(context) { }
        public AssessmentController() {
            _assessmentRepository = new AssessmentRepository();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Assessment>>> GetAssessment()
        {
            return await _assessmentRepository.GetAll();
            //return await _context.Assessment.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Assessment>> GetAssessment(int id)
        {
            return await _assessmentRepository.Get(id);
            //return await Get(id);
        }

        [HttpGet("{id}/detail")]
        public async Task<ActionResult<AssessmentDetail>> GetDetail(int id)
        {
            if (!_assessmentRepository.Exists(id))
            {
                return new BadRequestResult();
            }
            return await _assessmentRepository.GetDetail(id);
            /*
            if (!Exists(id))
            {
                return BadRequest();
            }

            var model = await _context.AssessmentDetail.FromSqlRaw("EXEC spAssessmentDetail @AssessmentId={0}", id).ToListAsync();

            if (model == null)
            {
                return NotFound();
            }

            return model.First();
            */
        }

        [HttpGet("person/{personId}")]
        public async Task<ActionResult<IEnumerable<AssessmentDetail>>> GetByPerson(int personId)
        {
            return await _assessmentRepository.GetByPerson(personId);
            //return await _context.AssessmentDetail.FromSqlRaw("EXEC spAssessmentDetail @PersonId={0}", personId).ToListAsync();
        }

        [HttpGet("person/{personId}/draft/{voicePersonId}")]
        public async Task<ActionResult<PersonAssessmentDetail>> GetDraft(int personId, int voicePersonId)
        {
            return await _assessmentRepository.GetDraft(personId, voicePersonId);
            /*
            return await _context.PersonAssessmentDetail
                .FromSqlRaw("EXEC spPersonAssessmentDetail @PersonId={0}, @VoicePersonId={1}, @IsDraft={2}", personId, voicePersonId, 1)
                .FirstOrDefaultAsync();
            */
        }

        [HttpGet("person/{personId}/detail")]
        public async Task<ActionResult<IEnumerable<PersonAssessmentDetail>>> GetDetailByPerson(int personId)
        {
            return await _assessmentRepository.GetDetailByPerson(personId);
            //return await _context.PersonAssessmentDetail.FromSqlRaw("EXEC spPersonAssessmentDetail {0}", personId).ToListAsync();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<int>> PutAssessment(int id, Assessment assessment)
        {
            return await _assessmentRepository.Put(id, assessment);
            //return await Put(id, assessment);
        }

        [HttpPost]
        public async Task<ActionResult<int>> PostAssessment(Assessment assessment)
        {
            await _assessmentRepository.Post(assessment);
            return assessment.AssessmentId;
            //return await Post(assessment);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> DeleteAssessment(int id)
        {
            return await _assessmentRepository.Delete(id);
            //return await Delete(id);
        }

        /*
        protected override bool Exists(int id)
        {
            return _assessmentRepository.Exists(id);
            //return _context.Assessment.Any(e => e.AssessmentId == id);
        }

        protected override async Task<Assessment> FindAsync(int id)
        {
            return await _context.Assessment.FindAsync(id) as Assessment;
        }
        */
    }
}
