using System.ComponentModel.DataAnnotations;
namespace RiseRestApi.Models
{
    public class AreaAccessDetail
    {
        [Key]
        public int AreaAccessId { get; set; }
        public string AreaName { get; set; }
        public int? PersonId { get; set; }
        public int? ProgramId { get; set; }
        public int? OrganizationId { get; set; }
    }
}
