using System;
using System.ComponentModel.DataAnnotations;

namespace RiseRestApi.Models
{
    public class Coach : IModel
    {
        [Key]
        public int CoachId { get; set; }
        public int PupilPersonId { get; set; }
        public int CoachPersonId { get; set; }
        public int ProgamId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public Person CoachPerson { get; set; }
        public Person PupilPerson { get; set; }
        public RiseProgram Program { get; set; }

        public int Id => CoachId;
    }
}
