namespace RiseRestApi.Models
{
    public partial class SchoolGrid
    {
        public int SchoolId { get; set; }
        public string SchoolName { get; set; }
        public int AdminPersonId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public int EntrepreneurCount { get; set; }
        public int CoachCount { get; set; }
        public int ProgramCount { get; set; }
    }
}
