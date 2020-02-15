namespace RiseRestApi.Models
{
    public class CoachFirstLatestScoreChart
    {
        public int PersonId { get; set; }
        public int FirstScore { get; set; }
        public int LatestScore { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
    }
}
