using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RiseRestApi.Models
{
    public partial class RiseProgram
    {
        [Key]
        public int ProgramId { get; set; }
        public string ProgramName { get; set; }
        public int AdminPersonId { get; set; }
        public int ScholId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsRemoved { get; set; }

        public Person AdminPerson { get; set; }
        public School School { get; set; }
        public ICollection<Person> Students { get; set; }
    }
}
