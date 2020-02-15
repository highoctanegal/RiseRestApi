using System.ComponentModel.DataAnnotations;

namespace RiseRestApi.Models
{
    public partial class AssessmentResponse : AssessmentResponseBase, IModel
    {
        [Key]
        public int AssessmentResponseId { get; set; }

        public virtual Assessment Assessment { get; set; }
        public virtual Note Note { get; set; }
        public virtual Rating Rating { get; set; }

        public int Id => AssessmentResponseId;
    }
}
