using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RiseRestApi.Models
{
    public partial class SkillSet : IModel
    {
        public SkillSet()
        {
            SurveyQuestion = new HashSet<SurveyQuestion>();
        }

        [Key]
        public int SkillSetId { get; set; }
        public string SkillSetName { get; set; }

        public virtual ICollection<SurveyQuestion> SurveyQuestion { get; set; }

        public int Id => SkillSetId;
    }
}
