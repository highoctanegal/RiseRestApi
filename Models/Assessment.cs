using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RiseRestApi.Models
{
    public partial class Assessment : AssessmentBase, IModel
    {
        public Assessment()
        {
            AssessmentResponse = new HashSet<AssessmentResponse>();
        }
        
        [Key]
        public int AssessmentId { get; set; }

        public virtual Person Person { get; set; }
        public virtual Note Note { get; set; }
        public virtual Survey Survey { get; set; }
        public virtual Voice Voice { get; set; }
        public virtual ICollection<AssessmentResponse> AssessmentResponse { get; set; }

        public int Id => AssessmentId;
    }
}
