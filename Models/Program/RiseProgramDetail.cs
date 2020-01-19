namespace RiseRestApi.Models
{
    public class RiseProgramDetail : RiseProgramBase
    {
        public int ProgramId { get; set; }
        public int AddressId { get; set; }
        public string OrganizationName { get; set; }
        public string Website { get; set; }
        public string BlobImageName { get; set; }
        public string AdminPhone { get; set; }
        public string AdminEmail { get; set; }
        public string AdminFirstName { get; set; }
        public string AdminLastName { get; set; }
        public int AssessmentCount { get; set; }
        public int EntrepreneurCount { get; set; }
        public int CoachCount { get; set; }

        public string AdminFullName => $"{AdminFirstName} {AdminLastName}";
    }
}
