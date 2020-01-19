using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace RiseRestApi.Models
{
    public partial class Organization : OrganizationBase, IModel
    {
        public Organization()
        {
            Programs = new HashSet<RiseProgram>();
            Students = new HashSet<Person>();
        }
        
        [Key]
        public int OrganizationId { get; set; }
        
        public Person Admin { get; set; }
        public Address Address { get; set; }

        public ICollection<RiseProgram> Programs { get; set; }
        public ICollection<Person> Students { get; set; }

        public int Id => OrganizationId;
    }
}
