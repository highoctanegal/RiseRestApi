using System.ComponentModel.DataAnnotations;

namespace RiseRestApi.Models
{
    public partial class Person : IModel
    {
        [Key]
        public int PersonId { get; set; }
        public int AddressId { get; set; }
        public int SchoolId { get; set; }
        public int RoleId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public string Email { get; set; }
        public string Phone { get; set; }
        public string BlobImageName { get; set; }
        public bool IsRemoved { get; set; }

        public virtual Address Address { get; set; }
        public virtual School School { get; set; }
        public virtual Role Role { get; set; }
        public int Id => PersonId;
    }
}
