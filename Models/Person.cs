using System;
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
        }

        [Key]
        public int PersonId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool IsEntrepeneur { get; set; }
        public bool IsCoach { get; set; }
        public int? CurrentBusinessId { get; set; }
        public int BusinessCount { get; set; }
        public int Level { get; set; }
        public bool IsActive { get; set; }
        public bool IsRemoved { get; set; }

        public virtual Business CurrentBusiness { get; set; }
        public virtual ICollection<Assessment> AssessmentAssessingPerson { get; set; }
        public virtual ICollection<Assessment> AssessmentCoachPerson { get; set; }
        public virtual ICollection<Assessment> AssessmentRegardingPerson { get; set; }
        public virtual ICollection<Note> NoteAuthorPerson { get; set; }
        public virtual ICollection<Note> NoteReferencePerson { get; set; }

        public int Id => PersonId;
    }
}
