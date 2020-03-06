using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace RiseRestApi.Models
{
    public class PersonAssessmentDetail
    {
        public int AssessmentId { get; set; }
        public DateTime? AssessmentDate { get; set; }
        public int VoiceId { get; set; }
        public int? OverallLevel { get; set; }
        public int? OverallScore { get; set; }
        public string VoiceName { get; set; }
        public string VoiceTitle { get; set; }
        public string HeaderScores { get; set; }
        public string QuestionScores { get; set; }
        public int NoteCount { get; set; }
        public string NoteQuestionIds { get; set; }


        [NotMapped]
        public IEnumerable<int> NoteQuestionIdList { 
            get {
                return string.IsNullOrEmpty(NoteQuestionIds) 
                    ? new List<int>() 
                    : NoteQuestionIds.Split(',').Select(int.Parse);
            } 
        }

        private IDictionary<int, int> _headerScoreDict;
        private IDictionary<int, Rating> _questionScoreDict;

        [NotMapped]
        public IDictionary<int, int> HeaderScoreDict
        {
            get
            {
                if (_headerScoreDict != null)
                {
                    return _headerScoreDict;
                }

                _headerScoreDict = new Dictionary<int, int>();
                if (HeaderScores != null)
                {
                    var skillScores = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<SkillScores>>(HeaderScores);
                    foreach (var ss in skillScores)
                    {
                        _headerScoreDict.Add(ss.SkillSetId, ss.SkillSetScore);
                    }
                }
                return _headerScoreDict;
            }
        }

        [NotMapped]
        public IDictionary<int, Rating> QuestionScoreDict
        {
            get
            {
                if (_questionScoreDict != null)
                {
                    return _questionScoreDict;
                }

                _questionScoreDict = new Dictionary<int, Rating>();

                if (QuestionScores != null)
                {
                    var questionScores = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<QuestionScore>>(QuestionScores);
                    foreach (var qs in questionScores)
                    {
                        _questionScoreDict.Add(qs.QuestionId, new Rating { RatingId = qs.RatingId, Score = qs.Score });
                    }
                }
                return _questionScoreDict;
            }
        }

        private class QuestionScore
        {
            public int QuestionId { get; set; }
            public int RatingId { get; set; }
            public int Score { get; set; }
        }

        private class SkillScores
        {
            public int SkillSetId { get; set; }
            public int SkillSetScore { get; set; }
        }
    }
}
