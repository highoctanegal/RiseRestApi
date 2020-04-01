using Microsoft.EntityFrameworkCore;
using RiseRestApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RiseRestApi.Repository
{
    public class AssessmentRepository : IAssessmentRepository
    {
        private readonly RiseContext _context;
        public AssessmentRepository()
        {
            _context = new RiseContext();
        }
        public AssessmentRepository(RiseContext context)
        {
            _context = context;
        }

        public Task<int> Delete(int id)
        {
            var assessment = _context.Assessment.Find(id);
            if (assessment != null)
            {
                assessment.IsRemoved = true;
                return _context.SaveChangesAsync();
            }
            return null;
        }

        public Task<Assessment> Get(int id)
        {
            return _context.Assessment.FirstOrDefaultAsync(a => a.AssessmentId == id);
        }

        public Task<List<Assessment>> GetAll()
        {
            return _context.Assessment.Where(a => !a.IsRemoved).ToListAsync();
        }

        public Task<AssessmentDetail> GetDetail(int id)
        {
            return _context.AssessmentDetail.FromSqlRaw("EXEC spAssessmentDetail @AssessmentId={0}", id).FirstOrDefaultAsync();
        }

        public Task<int> Post(Assessment assessment)
        {
            if (assessment == null)
            {
                return null;
            }
            _context.Add(assessment);
            return _context.SaveChangesAsync();
        }

        public Task<int> Put(int id, Assessment assessment)
        {
            _context.Entry(assessment).State = EntityState.Modified;
            return _context.SaveChangesAsync();
        }

        public Task<List<AssessmentDetail>> GetByPerson(int personId)
        {
            return _context.AssessmentDetail.FromSqlRaw("EXEC spAssessmentDetail @PersonId={0}", personId).ToListAsync();
        }

        public Task<PersonAssessmentDetail> GetDraft(int personId, int voicePersonId)
        {
            return _context.PersonAssessmentDetail
                .FromSqlRaw("EXEC spPersonAssessmentDetail @PersonId={0}, @VoicePersonId={1}, @IsDraft={2}", personId, voicePersonId, 1)
                .FirstOrDefaultAsync();
        }

        public Task<List<PersonAssessmentDetail>> GetDetailByPerson(int personId)
        {
            return _context.PersonAssessmentDetail.FromSqlRaw("EXEC spPersonAssessmentDetail {0}", personId).ToListAsync();
        }

        public bool Exists(int id)
        {
            return _context.Assessment.Any(e => e.AssessmentId == id);
        }
    }
}
