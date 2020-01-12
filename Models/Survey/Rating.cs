using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RiseRestApi.Models
{
    public partial class Rating : IModel
    {
        public Rating()
        {
            AssessmentResponse = new HashSet<AssessmentResponse>();
        }

        [Key]
        public int RatingId { get; set; }
        public int Score { get; set; }
        public string RatingText { get; set; }
        public int QuestionId { get; set; }
        public int RatingOrder { get; set; }

        public virtual Question Question { get; set; }
        public virtual ICollection<AssessmentResponse> AssessmentResponse { get; set; }

        public int Id => RatingId;
    }
}
