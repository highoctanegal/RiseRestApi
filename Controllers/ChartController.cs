using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RiseRestApi.Models;
using RiseRestApi.Repository;
using RiseRestApi.Util;
using System.Threading.Tasks;

namespace RiseRestApi.Controllers
{
    [EnableCors("AllowAll")]
    [Route("api/[controller]")]
    [ApiController]
    public class ChartController : ControllerBase
    {
        protected readonly RiseContext _context;

        public ChartController(RiseContext context)
        {
            _context = context;
        }

        [HttpGet("personassessment/{personId}")]
        public async Task<ActionResult<ChartData>> GetPersonAssessmentChart(int personId)
        {
            return await GetPersonAssessmentChart(personId, 1);

        }

        [HttpGet("personassessment/{personId}/{voiceId}")]
        public async Task<ActionResult<ChartData>> GetPersonAssessmentChart(int personId, int voiceId)
        {
            var skillSetLevels = await _context.PersonAssessmentChart.FromSqlRaw($"EXEC spPersonAssessmentChart {personId}, {voiceId}").ToListAsync();
            var chartDataConverter = new ChartDataConverter(skillSetLevels);
            return chartDataConverter.ChartData;
        }

        [HttpGet("coachskillsetpercentage/{coachPersonId}")]
        public async Task<ActionResult<ChartData>> GetCoachSkillSetPercentageChart(int coachPersonId)
        {
            var skillSetPercentages = await _context.SkillSetPercentageChart.FromSqlRaw($"EXEC spCoachSkillSetPercentageChart {coachPersonId}").ToListAsync();
            var chartDataConverter = new ChartDataConverter(skillSetPercentages);
            return chartDataConverter.ChartData;
        }

        [HttpGet("coachfirstlatestscore/{coachPersonId}")]
        public async Task<ActionResult<ChartData>> GetCoachFirstLastScoreChart(int coachPersonId)
        {
            var firstLastLevels = await _context.CoachCoachFirstLatestScoreChart.FromSqlRaw($"EXEC spCoachFirstLatestScoreChart {coachPersonId}").ToListAsync();
            var chartDataConverter = new ChartDataConverter(firstLastLevels);
            return chartDataConverter.ChartData;
        }

        [HttpGet("organizationskillsetpercentage/{organizationId}")]
        public async Task<ActionResult<ChartData>> GetOrganizationSkillSetPercentageChart(int organizationId)
        {
            var skillSetPercentages = await _context.SkillSetPercentageChart.FromSqlRaw($"EXEC spOrganizationSkillSetPercentageChart {organizationId}").ToListAsync();
            var chartDataConverter = new ChartDataConverter(skillSetPercentages);
            return chartDataConverter.ChartData;
        }

        [HttpGet("programskillsetpercentage/{programId}")]
        public async Task<ActionResult<ChartData>> GetProgramSkillSetPercentageChart(int programId)
        {
            var skillSetPercentages = await _context.SkillSetPercentageChart.FromSqlRaw($"EXEC spProgramSkillSetPercentageChart {programId}").ToListAsync();
            var chartDataConverter = new ChartDataConverter(skillSetPercentages);
            return chartDataConverter.ChartData;
        }
    }
}