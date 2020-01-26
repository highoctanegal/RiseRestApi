using System;

namespace RiseRestApi.Models
{
    public class AssessmentResponseDetail : AssessmentResponseBase
    {
        public int AssessmentResponseId { get; set; }
        public int Score { get; set; }
        public string RatingText { get; set; }
        public int QuestionId { get; set; }
        public int RatingOrder { get; set; }
        public string NoteText { get; set; }
        public DateTime? NoteDate { get; set; }
        public string NoteAuthorPersonId { get; set; }
        public string NoteAuthorFirstName { get; set; }
        public string NoteAuthorLastName { get; set; }
        public string NoteAuthorFullName => $"{NoteAuthorFirstName} {NoteAuthorLastName}";
    }
}
