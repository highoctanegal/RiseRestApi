using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RiseRestApi.Models
{
    public partial class Person : IModel
    {
        public Person()
        {
            AssessmentAssessingPerson = new HashSet<Assessment>();
            AssessmentCoachPerson = new HashSet<Assessment>();
            AssessmentRegardingPerson = new HashSet<Assessment>();
            NoteAuthorPerson = new HashSet<Note>();
            NoteReferencePerson = new HashSet<Note>();
            //Coaches = new HashSet<Person>();
            PersonRoles = new HashSet<PersonRole>();
            PersonPrograms = new HashSet<PersonProgram>();
            Organizations = new HashSet<Organization>();
        }

        [Key]
        public int PersonId { get; set; }
        public int AddressId { get; set; }
        public int? CurrentOrganizationId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public string Email { get; set; }
        public string Phone { get; set; }
        public int OrganizationCount => Organizations?.Count ?? 0; 
        public int? Level { get; set; }
        public bool IsRemoved { get; set; }

        public virtual Address Address { get; set; }
        public virtual Organization CurrentOrganization { get; set; }
        public virtual ICollection<Assessment> AssessmentAssessingPerson { get; set; }
        public virtual ICollection<Assessment> AssessmentCoachPerson { get; set; }
        public virtual ICollection<Assessment> AssessmentRegardingPerson { get; set; }
        public virtual ICollection<Note> NoteAuthorPerson { get; set; }
        public virtual ICollection<Note> NoteReferencePerson { get; set; }
        public virtual ICollection<Organization> Organizations { get; set; }
        //public virtual ICollection<Coach> Coaches { get; set; }
        public virtual ICollection<PersonRole> PersonRoles { get; set; }
        public virtual ICollection<PersonProgram> PersonPrograms { get; set; }
        public int Id => PersonId;
    }
}
