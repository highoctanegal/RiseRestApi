using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RiseRestApi.Models
{
    public partial class Question : IModel
    {
        public Question()
        {
            Rating = new HashSet<Rating>();
            SurveyQuestion = new HashSet<SurveyQuestion>();
        }

        [Key]
        public int QuestionId { get; set; }
        public string QuestionText { get; set; }
        public string QuestionDescription { get; set; }
        public bool IsRemoved { get; set; }

        public virtual ICollection<Rating> Rating { get; set; }
        public virtual ICollection<SurveyQuestion> SurveyQuestion { get; set; }

        public int Id => QuestionId;
    }
}
