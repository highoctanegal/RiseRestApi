namespace RiseRestApi.Models
{
    public class NoteDetail : NoteBase
    {
        public int NoteId { get; set; }
        public string AuthorFirstName { get; set; }
        public string AuthorLastName { get; set; }
        public string AuthorFullName => $"{AuthorFirstName} {AuthorLastName}";
        public string ReferenceFirstName { get; set; }
        public string ReferenceLastName { get; set; }
        public string ReferenceFullName => $"{ReferenceFirstName} {ReferenceLastName}";
        public int? AssessmentId { get; set; }
        public int? AssessmentResponseId { get; set; }
        public int? QuestionId { get; set; }
        public string Detail { get; set; }
    }
}
