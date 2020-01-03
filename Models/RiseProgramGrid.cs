
namespace RiseRestApi.Models
{ 
    public class RiseProgramGrid
    {
        public int ProgramId { get; set; }
        public string ProgramName { get; set; }
        public int? SchoolId { get; set; }
        public string SchoolName { get; set; }
        public int EntrepreneurCount { get; set; }
        public int CoachCount { get; set; }
        public int AdminPersonId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string FullName => $"{FirstName} {LastName}";
    }
}
