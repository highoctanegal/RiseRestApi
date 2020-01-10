namespace RiseRestApi.Models
{
    public class PersonDetail : PersonBase, IModel
    {
        public int PersonId { get; set; }
        public string SchoolName { get; set; }
        public string ProgramName { get; set; }
        public string RoleName { get; set; }
        public string CoachFirstName { get; set; }
        public string CoachLastName { get; set; }
        public int? TransformationLevel { get; set; }
        public int? RelationshipLevel { get; set; }
        public int? BusinessLevel { get; set; }
        public int? OrganizationProcessLevel { get; set; }
        public int? OverallLevel { get; set; }

        public string CoachFullName => $"{CoachFirstName} {CoachLastName}";

        public int Id => PersonId;
    }
}
