namespace RiseRestApi.Models
{
    public class PersonBase
    {
        public int? AddressId { get; set; }
        public int? SchoolId { get; set; }
        public int? ProgramId { get; set; }
        public int? CoachId { get; set; }
        public int RoleId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public string Email { get; set; }
        public string Phone { get; set; }
        public string BusinessName { get; set; }
        public string BusinessWebsite { get; set; }
        public string BlobImageName { get; set; }
        public decimal? SalesGenerated { get; set; }
        public decimal? OutsideInvestment { get; set; }
        public int? JobsCreated { get; set; }
        public bool IsRemoved { get; set; }
    }
}
