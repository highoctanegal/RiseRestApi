using System.ComponentModel.DataAnnotations;

namespace RiseRestApi.Models
{
    public class PersonRole : IModel
    {
        [Key]
        public int PersonRoleId { get; set; }
        public int PersonId { get; set; }
        public int RoleId { get; set; }

        public Person Person { get; set; }
        public Role Role { get; set; }

        public int Id => PersonRoleId;
    }
}
