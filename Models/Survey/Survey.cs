using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RiseRestApi.Models
{
    public partial class Survey : IModel
    {
        public Survey()
        {
            Assessment = new HashSet<Assessment>();
            SurveyQuestion = new HashSet<SurveyQuestion>();
        }

        [Key]
        public int SurveyId { get; set; }
        public string SurveyName { get; set; }
        public bool IsRemoved { get; set; }

        public virtual ICollection<Assessment> Assessment { get; set; }
        public virtual ICollection<SurveyQuestion> SurveyQuestion { get; set; }
        public int Id => SurveyId;
    }
}
