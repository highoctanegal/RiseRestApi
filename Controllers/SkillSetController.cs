using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RiseRestApi.Models;
using RiseRestApi.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;

namespace RiseRestApi.Controllers
{
    [EnableCors("AllowAll")]
    [Route("api/[controller]")]
    [ApiController]
    public class SkillSetController : BaseApiController<SkillSet>
    {
        public SkillSetController(RiseContext context) : base(context) { }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SkillSet>>> GetSkillSet()
        {
            return await _context.SkillSet.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SkillSet>> GetSkillSet(int id)
        {
            return await Get(id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutSkillSet(int id, SkillSet skillSet)
        {
            return await Put(id, skillSet);
        }

        [HttpPost]
        public async Task<ActionResult<SkillSet>> PostSkillSet(SkillSet skillSet)
        {
            return await Post(skillSet);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<SkillSet>> DeleteSkillSet(int id)
        {
            return await Delete(id);
        }

        protected override bool Exists(int id)
        {
            return _context.SkillSet.Any(e => e.SkillSetId == id);
        }

        protected override async Task<SkillSet> FindAsync(int id)
        {
            return await _context.SkillSet.FindAsync(id) as SkillSet;
        }

    }
}
