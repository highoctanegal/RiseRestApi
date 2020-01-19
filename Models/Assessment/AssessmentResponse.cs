using System.ComponentModel.DataAnnotations;

namespace RiseRestApi.Models
{
    public partial class AssessmentResponse
    {
        [Key]
        public int AssessmentResponseId { get; set; }
        public int AssessmentId { get; set; }
        public int? NoteId { get; set; }
        public int RatingId { get; set; }

        public virtual Assessment Assessment { get; set; }
        public virtual Note Note { get; set; }
        public virtual Rating Rating { get; set; }

        public int Id => AssessmentResponseId;
    }
}
