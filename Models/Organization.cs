using System.ComponentModel.DataAnnotations;

namespace RiseRestApi.Models
{
    public partial class Organization : IModel
    {
        [Key]
        public int OrganizationId { get; set; }
        public string OrganizationName { get; set; }
        public bool IsRemoved { get; set; }
        public int Id { get => OrganizationId; }
    }
}
