using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RiseRestApi.Models
{
    public partial class Person : PersonBase, IModel
    {
        [Key]
        public int PersonId { get; set; }

        public virtual Address Address { get; set; }
        public virtual Organization Organization { get; set; }
        public virtual Role Role { get; set; }
        public virtual Person Coach { get; set; }
        public virtual RiseProgram Program { get; set; }

        public ICollection<Person> Pupils { get; set; }
        public ICollection<Organization> Organizations { get; set; }

        public int Id => PersonId;
    }
}
