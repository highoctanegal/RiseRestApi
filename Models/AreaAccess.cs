using System.ComponentModel.DataAnnotations;

namespace RiseRestApi.Models
{
    public class AreaAuthorization
    {
        [Key]
        public string AreaAuthorizationId { get; set; }
        public string RoleId { get; set; }
        public string AreaId { get; set; }
        public bool? PersonIdOnly { get; set; }
        public bool? ProgramIdOnly { get; set; }
        public bool? OrganizationIdOnly { get; set; }
    }
}
