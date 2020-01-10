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

        [HttpGet("coachskillsetpercentage/{coachId}")]
        public async Task<ActionResult<ChartData>> GetCoachSkillSetPercentageChart(int coachId)
        {
            var skillSetPercentages = await _context.CoachSkillSetPercentageChart.FromSqlRaw($"EXEC spCoachSkillSetPercentageChart {coachId}").ToListAsync();
            var chartDataConverter = new ChartDataConverter(skillSetPercentages);
            return chartDataConverter.ChartData;
        }
    }
}