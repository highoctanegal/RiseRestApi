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
        public async Task<ActionResult<AssessmentGrid>> GetAssessmentDetail(int id)
        {
            if (!Exists(id))
            {
                return NotFound();
            }

            var model = await _context.AssessmentGrid.FromSqlRaw("EXEC spAssessmentGrid NULL,{0}", id).ToListAsync();

            if (model == null)
            {
                return NotFound();
            }

            return model.First();
        }

        [HttpGet("{id}/responsedetail")]
        public async Task<ActionResult<IEnumerable<AssessmentResponseDetail>>> GetAssessmentResponseDetail(int id)
        {
            if (!Exists(id))
            {
                return NotFound();
            }

            return await _context.AssessmentResponseDetail.FromSqlRaw("EXEC spAssessmentResponseDetail {0}", id)
                .ToListAsync();
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
