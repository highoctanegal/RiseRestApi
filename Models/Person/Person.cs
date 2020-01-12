using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RiseRestApi.Models
{
    public partial class Person : PersonBase, IModel
    {
        [Key]
        public int PersonId { get; set; }

        public virtual Address Address { get; set; }
        [NotMapped]
        public virtual School School { get; set; }
        public virtual Role Role { get; set; }
        public virtual Person Coach { get; set; }
        public virtual RiseProgram Program { get; set; }

        public ICollection<Person> Pupils { get; set; }
        public ICollection<School> Schools { get; set; }

        public int Id => PersonId;
    }
}
