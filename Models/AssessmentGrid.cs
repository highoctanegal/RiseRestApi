namespace RiseRestApi.Models
{
    public class AssessmentGrid : AssessmentBase
    {
        public int AssessmentId { get; set; }
        public string VoiceText { get; set; }
        public string NoteText { get; set; }
        public string AuthorPersonId { get; set; }
        public string AuthorFirstName { get; set; }
        public string AuthorLastName { get; set; }
        public string AuthorFullName => $"{AuthorFirstName} {AuthorLastName}";
        public string VoiceFirstName { get; set; }
        public string VoiceLastName { get; set; }
        public string VoiceFullName => $"{VoiceFirstName} {VoiceLastName}";
    }
}
