using System.ComponentModel.DataAnnotations;
namespace RiseRestApi.Models
{
    public class AreaAuthorizationDetail
    {
        [Key]
        public int AreaAuthorizationId { get; set; }
        public string AreaName { get; set; }
        public int? PersonId { get; set; }
        public int? ProgramId { get; set; }
        public int? OrganizationId { get; set; }
    }
}
