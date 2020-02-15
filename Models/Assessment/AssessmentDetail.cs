using System;

namespace RiseRestApi.Models
{
    public class AssessmentDetail : AssessmentBase
    {
        public int AssessmentId { get; set; }
        public string VoiceName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public DateTime? NoteDate { get; set; }
        public string NoteText { get; set; }
        public string NoteAuthorPersonId { get; set; }
        public string NoteAuthorFirstName { get; set; }
        public string NoteAuthorLastName { get; set; }
        public string NoteAuthorFullName => $"{NoteAuthorFirstName} {NoteAuthorLastName}";
        public string VoiceFirstName { get; set; }
        public string VoiceLastName { get; set; }
        public string VoiceFullName => $"{VoiceFirstName} {VoiceLastName}";
        public string SurveyName { get; set; }
    }
}
