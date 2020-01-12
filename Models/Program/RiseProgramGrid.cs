
namespace RiseRestApi.Models
{ 
    public class RiseProgramGrid : RiseProgramBase
    {
        public int ProgramId { get; set; }
        public string SchoolName { get; set; }
        public int EntrepreneurCount { get; set; }
        public int CoachCount { get; set; }
        public string AdminFirstName { get; set; }
        public string AdminLastName { get; set; }

        public string AdminFullName => $"{AdminFirstName} {AdminLastName}";
    }
}
