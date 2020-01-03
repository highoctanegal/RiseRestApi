using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace RiseRestApi.Models
{
    public partial class School : IModel
    {
        public School()
        {
            Programs = new HashSet<RiseProgram>();
            Students = new HashSet<Person>();
        }
        
        [Key]
        public int SchoolId { get; set; }
        public int AddressId { get; set; }
        public int AdminPersonId { get; set; }
        public string SchoolName { get; set; }
        public string BlobImageName { get; set; }
        public bool IsRemoved { get; set; }

        public Person Admin { get; set; }
        public Address Address { get; set; }

        public ICollection<RiseProgram> Programs { get; set; }
        public ICollection<Person> Students { get; set; }

        public int Id => SchoolId;
    }
}
