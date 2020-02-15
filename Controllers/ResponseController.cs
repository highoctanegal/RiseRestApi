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
    public class ResponseController : BaseApiController<AssessmentResponse>
    {
        public ResponseController(RiseContext context) : base(context) { }

        [HttpGet("assessment/{assessmentId}")]
        public ActionResult<IEnumerable<AssessmentResponse>> GetAssessmentResponse(int assessmentId)
        {
            return _context.AssessmentResponse.Where(a => a.AssessmentId == assessmentId).ToList();
        }

        [HttpGet("assessment/{assessmentId}/detail")]
        public async Task<ActionResult<IEnumerable<AssessmentResponseDetail>>> GetAssessmentResponseDetail(int assessmentId)
        {
            return await _context.AssessmentResponseDetail.FromSqlRaw("EXEC spAssessmentResponseDetail {0}", assessmentId)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AssessmentResponse>> GetResponse(int id)
        {
            return await Get(id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutResponse(int id, AssessmentResponse response)
        {
            return await Put(id, response);
        }

        [HttpPost]
        public async Task<ActionResult<AssessmentResponse>> PostResponse(AssessmentResponse response)
        {
            return await Post(response);
        }

        [HttpPut("saveall/{assessmentId}")]
        public async Task<ActionResult<int>> SaveResponses(int assessmentId, object responses)
        {
            var jobject = responses as Newtonsoft.Json.Linq.JObject;
            var r = jobject.First;
            AssessmentResponse response;
            while (r != null)
            {
                var questionId = int.Parse(r.Path);

                response = _context.AssessmentResponse.FirstOrDefault(a => a.Rating.QuestionId == questionId && a.AssessmentId == assessmentId);

                if (response == null)
                {
                    response = new AssessmentResponse
                    {
                        AssessmentId = assessmentId,
                        RatingId = (int)r.First
                    };
                    _context.AssessmentResponse.Add(response);
                }
                else
                {
                    response.RatingId = (int)r.First;
                }

                r = r.Next;
            }

            return await _context.SaveChangesAsync();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<AssessmentResponse>> DeleteResponse(int id)
        {
            return await Delete(id);
        }

        [HttpDelete("all/{assessmentId}")]
        public async Task<ActionResult<int>> DeleteAll(int assessmentId)
        {
            var list = _context.AssessmentResponse.Where(r => r.AssessmentId == assessmentId).ToList();
            _context.AssessmentResponse.RemoveRange(list);
            return await _context.SaveChangesAsync();
        }

        protected override bool Exists(int id)
        {
            return _context.AssessmentResponse.Any(e => e.AssessmentResponseId == id);
        }

        protected override async Task<AssessmentResponse> FindAsync(int id)
        {
            return await _context.AssessmentResponse.FindAsync(id) as AssessmentResponse;
        }
    }
}