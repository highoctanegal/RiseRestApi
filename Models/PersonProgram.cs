using System;
using System.ComponentModel.DataAnnotations;

namespace RiseRestApi.Models
{
    public class PersonProgram : IModel
    {
        [Key]
        public int PersonProgramId { get; set; }
        public int PersonId { get; set; }
        public int ProgramId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool Completed { get; set; }

        public Person Person { get; set; }
        public RiseProgram Program { get; set; }

        public int Id => PersonProgramId;
    }
}
