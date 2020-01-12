
namespace RiseRestApi.Models
{ 
    public class PersonGrid
    {
        public int PersonId { get; set; }
        public int? SchoolId { get; set; }
        public int? ProgramId { get; set; }
        public int RoleId { get; set; }
        public int? OverallLevel { get; set; }
        public int? FirstScore { get; set; }
        public int? LatestScore { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SchoolName { get; set; }
        public string ProgramName { get; set; }
        public string RoleName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
    }
}
