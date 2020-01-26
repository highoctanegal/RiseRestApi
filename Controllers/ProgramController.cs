using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RiseRestApi.Repository;
using RiseRestApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Cors;

namespace RiseRestApi.Controllers
{
    [EnableCors("AllowAll")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProgramController : BaseApiController<RiseProgram>
    {
        public ProgramController(RiseContext context) : base(context) { }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RiseProgram>>> GetProgram()
        {
            return await _context.Program.Where(p => !p.IsRemoved).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RiseProgram>> GetProgram(int id)
        {
            return await Get(id);
        }

        [HttpGet("{id}/detail")]
        public async Task<ActionResult<RiseProgramDetail>> GetProgramDetail(int id)
        {
            if (!Exists(id))
            {
                return NotFound();
            }

            var model = await _context.ProgramDetail.FromSqlRaw("EXEC dbo.spProgramDetail {0}", id).ToListAsync();

            if (model == null)
            {
                return NotFound();
            }

            return model.First();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProgram(int id, RiseProgram program)
        {
            return await Put(id, program);
        }

        [HttpPost]
        public async Task<ActionResult<RiseProgram>> PostProgram(RiseProgram program)
        {
            return await Post(program);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<RiseProgram>> DeleteProgram(int id)
        {
            var program = _context.Program.Where(p => p.ProgramId == id).FirstOrDefault();
            if (program == null)
            {
                return new NotFoundResult();
            }
            program.IsRemoved = true;
            await Put(id, program);
            return new JsonResult(program);

        }

        protected override bool Exists(int id)
        {
            return _context.Program.Any(e => e.ProgramId == id);
        }

        protected override async Task<RiseProgram> FindAsync(int id)
        {
            return await _context.Program.FindAsync(id) as RiseProgram;
        }
    }
}