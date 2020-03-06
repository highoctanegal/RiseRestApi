using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace RiseRestApi.Models
{
    public class PersonBase
    {
        public int? AddressId { get; set; }
        public int? OrganizationId { get; set; }
        public int? ProgramId { get; set; }
        public int? CoachPersonId { get; set; }
        public int RoleId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime? LastLogin { get; set; }
        public string BusinessName { get; set; }
        public string BusinessWebsite { get; set; }
        public string BlobImageName { get; set; }
        public decimal? SalesGenerated { get; set; }
        public decimal? OutsideInvestment { get; set; }
        public int? JobsCreated { get; set; }
        public bool IsRemoved { get; set; }
        [NotMapped]
        public string Token { get; set; }
    }
}
