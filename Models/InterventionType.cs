using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RiseRestApi.Models
{
    public partial class InterventionType
    {
        [Key]
        public int InterventionTypeId { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }
        public bool IsRemoved { get; set; }
    }
}
