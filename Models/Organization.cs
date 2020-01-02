using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace RiseRestApi.Models
{
    public partial class Organization : IModel
    {
        public Organization()
        {
            Programs = new HashSet<RiseProgram>();
        }
        
        [Key]
        public int OrganizationId { get; set; }
        public int AddressId { get; set; }
        public int AdminPersonId { get; set; }
        public string OrganizationName { get; set; }
        public bool IsRemoved { get; set; }

        public Person Admin { get; set; }

        public Address Address { get; set; }

        public ICollection<RiseProgram> Programs { get; set; }

        public int Id { get => OrganizationId; }
    }
}
