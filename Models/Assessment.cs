using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RiseRestApi.Models
{
    public partial class Assessment : IModel
    {
        public Assessment()
        {
            AssessmentResponse = new HashSet<AssessmentResponse>();
        }
        
        [Key]
        public int AssessmentId { get; set; }
        public int SurveyId { get; set; }
        public int PersonId { get; set; }
        public int VoiceId { get; set; }
        public int? NoteId { get; set; }
        public bool IsDraft { get; set; }
        public DateTime LastUpdateDate { get; set; }
        public DateTime? SubmitDate { get; set; }
        public bool IsRemoved { get; set; }

        public virtual Person Person { get; set; }
        public virtual Note Note { get; set; }
        public virtual Survey Survey { get; set; }
        public virtual Voice Voice { get; set; }
        public virtual ICollection<AssessmentResponse> AssessmentResponse { get; set; }

        public int Id => AssessmentId;
    }
}
