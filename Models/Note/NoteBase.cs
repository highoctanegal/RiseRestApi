using System;

namespace RiseRestApi.Models
{
    public partial class NoteBase
    {
        public int AuthorPersonId { get; set; }
        public int ReferencePersonId { get; set; }
        public string NoteText { get; set; }
        public DateTime NoteDate { get; set; }
    }
}
