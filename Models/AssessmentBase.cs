using System;

namespace RiseRestApi.Models
{
    public class AssessmentBase
    {
        public int SurveyId { get; set; }
        public int PersonId { get; set; }
        public int VoiceId { get; set; }
        public int? VoicePersonId { get; set; }
        public int? NoteId { get; set; }
        public bool IsDraft { get; set; }
        public DateTime LastUpdateDate { get; set; }
        public DateTime? SubmitDate { get; set; }
        public bool IsRemoved { get; set; }

    }
}
