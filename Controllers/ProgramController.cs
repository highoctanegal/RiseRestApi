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
    public class ProgramController : BaseApiController
    {
        public ProgramController(RiseContext context) : base(context) { }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RiseProgram>>> GetProgram()
        {
            return await _context.Program.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IModel>> GetProgram(int id)
        {
            return await Get(id);
        }

        [HttpGet("detail/{id}")]
        public async Task<ActionResult<RiseProgramDetail>> GetProgramDetail(int id)
        {
            if (!Exists(id))
            {
                return NotFound();
            }

            var model = await _context.ProgramDetail.FromSqlRaw($"EXEC dbo.spProgramDetail {id}").ToListAsync();

            if (model == null)
            {
                return NotFound();
            }

            return model.First();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutRiseProgram(int id, RiseProgram program)
        {
            return await Put(id, program);
        }

        [HttpPost]
        public async Task<ActionResult<IModel>> PostRiseProgram(RiseProgram program)
        {
            return await Post(program);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<IModel>> DeleteRiseProgram(int id)
        {
            return await Delete(id);

        }

        public override bool Exists(int id)
        {
            return _context.Program.Any(e => e.ProgramId == id);
        }

        public override async Task<IModel> FindAsync(int id)
        {
            return await _context.Program.FindAsync(id) as IModel;
        }
    }
}