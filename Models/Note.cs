using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RiseRestApi.Models
{
    public partial class Note : IModel
    {
        public Note()
        {
            Assessment = new HashSet<Assessment>();
            AssessmentResponse = new HashSet<AssessmentResponse>();
        }

        [Key]
        public int NoteId { get; set; }
        public int AuthorPersonId { get; set; }
        public int ReferencePersonId { get; set; }
        public string NoteText { get; set; }
        public DateTime NoteDate { get; set; }

        public virtual Person AuthorPerson { get; set; }
        public virtual Person ReferencePerson { get; set; }
        public virtual ICollection<Assessment> Assessment { get; set; }
        public virtual ICollection<AssessmentResponse> AssessmentResponse { get; set; }

        public int Id => NoteId;
    }
}
