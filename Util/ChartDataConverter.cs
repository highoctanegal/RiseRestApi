using RiseRestApi.Models;
using System.Collections.Generic;
using System.Linq;
using static RiseRestApi.Models.ChartData;

namespace RiseRestApi.Util
{
    public class ChartDataConverter
    {
        public ChartData ChartData { get; private set; }

        public ChartDataConverter()
        {
            ChartData = new ChartData();

        }

        #region PersonAssessmentChart
        public ChartDataConverter(ICollection<PersonAssessmentChart> skillSetLevels) : this()
        {
            FillNames(skillSetLevels);
            FillData(skillSetLevels);
        }

        public void FillNames(ICollection<PersonAssessmentChart> skillSetLevels)
        {
            ChartData.CollectionNames = skillSetLevels.OrderBy(o => o.SubmitDate).Select(s => s.SubmitDate?.ToString("MM/dd/yyyy")).Distinct().ToList();
        }

        public void FillData(ICollection<PersonAssessmentChart> skillSetLevels)
        {
            var skillSetIds = skillSetLevels.Select(s => s.SkillSetId).Distinct().ToList();
            foreach (var skillSetId in skillSetIds)
            {
                var series = new Series()
                {
                    Name = skillSetLevels.FirstOrDefault(s => s.SkillSetId == skillSetId).SkillSetName,
                    Data = skillSetLevels.Where(s => s.SkillSetId == skillSetId).OrderBy(o => o.SubmitDate).Select(s => (object)s.SkillSetLevel ?? 0).ToList()
                };
                ChartData.SeriesData.Add(series);
            }
        }
        #endregion

        #region SkillSetPercentageChart
        public ChartDataConverter(ICollection<SkillSetPercentageChart> skillSetPercentages) : this()
        {
            FillNames(skillSetPercentages);
            FillData(skillSetPercentages);
        }

        public void FillNames(ICollection<SkillSetPercentageChart> skillSetPercentages)
        {
            ChartData.CollectionNames = skillSetPercentages.OrderBy(s => s.SkillSetId)
                .Select(s => s.SkillSetName.Replace(" ", "\n")).Distinct().ToList();
        }

        public void FillData(ICollection<SkillSetPercentageChart> skillSetPercentages)
        {
            for (int level = 5; level > 0; level--)
            {
                ChartData.SeriesData.Add(new Series
                {
                    Name = $"Level {level}",
                    Data = skillSetPercentages.Where(w => w.Level == level)
                    .OrderBy(t => t.SkillSetId)
                    .Select(s => (object)s.LevelPercentage).ToList()
                });
            }
        }
        #endregion

        #region CoachFirstLastLevelChart
        public ChartDataConverter(ICollection<CoachFirstLatestScoreChart> personLevels) : this()
        {
            FillNames();
            FillData(personLevels);
        }

        public void FillNames()
        {
            ChartData.CollectionNames = new List<string> { "First Assessment Score", "Latest Assessment Score" };
        }

        public void FillData(ICollection<CoachFirstLatestScoreChart> personLevels)
        {
            ChartData.SeriesData = personLevels.Select(s => new Series { 
                Name = s.PersonId.ToString(), 
                Data = new object[] { s.FirstScore, s.LatestScore } }).ToList();
        }
        #endregion
    }
}
