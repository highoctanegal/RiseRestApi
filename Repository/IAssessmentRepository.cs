using RiseRestApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RiseRestApi.Repository
{
    public interface IAssessmentRepository : IRiseRepository<Assessment>
    {
        public Task<AssessmentDetail> GetDetail(int id);
        public Task<List<AssessmentDetail>> GetByPerson(int personId);
        public Task<PersonAssessmentDetail> GetDraft(int personId, int voicePersonId);
        public Task<List<PersonAssessmentDetail>> GetDetailByPerson(int personId);
    }
}
