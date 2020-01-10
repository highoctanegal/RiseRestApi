﻿
namespace RiseRestApi.Models
{
    public class SchoolDetail : SchoolBase, IModel
    {
        public int SchoolId { get; set; }
        public string AdminFirstName { get; set; }
        public string AdminLastName { get; set; }
        public string AdminFullName => $"{AdminFirstName} {AdminLastName}";
        public string AdminPhone { get; set; }
        public string AdminEmail { get; set; }
        public int EntrepreneurCount { get; set; }
        public int CoachCount { get; set; }
        public int ProgramCount { get; set; }
        public int AssessmentCount { get; set; }

        public int Id => SchoolId;
    }
}
