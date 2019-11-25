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
        public int AssessingPersonId { get; set; }
        public int RegardingPersonId { get; set; }
        public int CoachPersonId { get; set; }
        public int? NoteId { get; set; }
        public int AssessmentStatusId { get; set; }
        public DateTime? ReviewDate { get; set; }
        public DateTime LastUpdateDate { get; set; }
        public DateTime? SubmitDate { get; set; }
        public bool IsRemoved { get; set; }

        public virtual Person AssessingPerson { get; set; }
        public virtual AssessmentStatus AssessmentStatus { get; set; }
        public virtual Person CoachPerson { get; set; }
        public virtual Note Note { get; set; }
        public virtual Person RegardingPerson { get; set; }
        public virtual Survey Survey { get; set; }
        public virtual ICollection<AssessmentResponse> AssessmentResponse { get; set; }

        public int Id => AssessmentId;
    }
}
