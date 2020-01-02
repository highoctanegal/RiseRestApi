using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RiseRestApi.Models
{
    public partial class RiseProgram
    {
        public RiseProgram()
        {
            PersonPrograms = new HashSet<PersonProgram>();
        }

        [Key]
        public int ProgramId { get; set; }
        public string ProgramName { get; set; }
        public int AdminPersonId { get; set; }
        public int OrganizationId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsRemoved { get; set; }

        public Person AdminPerson { get; set; }
        public Organization Organization { get; set; }
        public virtual ICollection<PersonProgram> PersonPrograms { get; set; }
    }
}
