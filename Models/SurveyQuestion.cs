using System.ComponentModel.DataAnnotations;

namespace RiseRestApi.Models
{
    public partial class SurveyQuestion : IModel 
    {
        [Key]
        public int SurveyQuestionId { get; set; }
        public int SurveyId { get; set; }
        public int QuestionId { get; set; }
        public int SkillSetId { get; set; }
        public int QuestionOrder { get; set; }

        public virtual Question Question { get; set; }
        public virtual SkillSet SkillSet { get; set; }
        public virtual Survey Survey { get; set; }

        public int Id => SurveyQuestionId;
    }
}
