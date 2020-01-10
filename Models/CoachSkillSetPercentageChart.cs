namespace RiseRestApi.Models
{
    public class CoachSkillSetPercentageChart
    {
        public int SkillSetId { get; set; }
        public string SkillSetName { get; set; }
        public int Level { get; set; }
        public decimal LevelPercentage { get; set; }
    }
}
