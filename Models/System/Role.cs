using System.ComponentModel.DataAnnotations;

namespace RiseRestApi.Models
{
    public class Role : IModel
    {
        [Key]
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public bool IsRemoved { get; set; }

        public int Id => RoleId;
    }
}
