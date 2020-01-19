using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RiseRestApi.Models
{
    public partial class RiseProgram : RiseProgramBase, IModel
    {
        [Key]
        public int ProgramId { get; set; }
        
        public Person AdminPerson { get; set; }
        public Organization Organization { get; set; }
        public ICollection<Person> Students { get; set; }

        public int Id => ProgramId;
    }
}
