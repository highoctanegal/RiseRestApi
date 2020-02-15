using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RiseRestApi.Models
{
    public partial class Note : NoteBase, IModel
    {
        public Note()
        {
            Assessment = new HashSet<Assessment>();
            AssessmentResponse = new HashSet<AssessmentResponse>();
        }

        [Key]
        public int NoteId { get; set; }

        public virtual Person AuthorPerson { get; set; }
        public virtual Person ReferencePerson { get; set; }
        public virtual ICollection<Assessment> Assessment { get; set; }
        public virtual ICollection<AssessmentResponse> AssessmentResponse { get; set; }

        public int Id => NoteId;
    }
}
