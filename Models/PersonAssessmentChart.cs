using System;

namespace RiseRestApi.Models
{
    public class PersonAssessmentChart
    {
        public int AssessmentId { get; set; }
        public int? SkillSetLevel { get; set; }
        public int SkillSetId { get; set; }
        public string SkillSetName { get; set; }
        public DateTime? SubmitDate { get; set; }
    }
}
