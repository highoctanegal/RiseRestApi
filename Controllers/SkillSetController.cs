using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RiseRestApi.Models;
using RiseRestApi.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RiseRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkillSetController : BaseApiController
    {
        public SkillSetController(RiseContext context) : base(context) { }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SkillSet>>> GetSkillSet()
        {
            return await _context.SkillSet.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IModel>> GetSkillSet(int id)
        {
            return await Get(id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutSkillSet(int id, SkillSet skillSet)
        {
            return await Put(id, skillSet);
        }

        [HttpPost]
        public async Task<ActionResult<IModel>> PostSkillSet(SkillSet skillSet)
        {
            return await Post(skillSet);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<IModel>> DeleteSkillSet(int id)
        {
            return await Delete(id);
        }

        public override bool Exists(int id)
        {
            return _context.SkillSet.Any(e => e.SkillSetId == id);
        }

        public override async Task<IModel> FindAsync(int id)
        {
            return await _context.SkillSet.FindAsync(id) as IModel;
        }

    }
}
