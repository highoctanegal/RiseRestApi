using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RiseRestApi.Models
{
    public partial class AssessmentStatus :  IModel
    {
        public AssessmentStatus()
        {
            Assessment = new HashSet<Assessment>();
        }

        [Key]
        public int AssessmentStatusId { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }
        public bool IsRemoved { get; set; }

        public virtual ICollection<Assessment> Assessment { get; set; }

        public int Id => AssessmentStatusId;
    }
}
