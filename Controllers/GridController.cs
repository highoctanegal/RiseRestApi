using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RiseRestApi.Models;
using RiseRestApi.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;

namespace RiseRestApi.Controllers
{
    [EnableCors("AllowAll")]
    [Route("api/[controller]")]
    [ApiController]
    public class GridController : ControllerBase
    {
        protected readonly RiseContext _context;

        public GridController(RiseContext context)
        {
            _context = context;
        }

        [HttpGet("users")]
        public async Task<ActionResult<IEnumerable<PersonGrid>>> GetPersons()
        {
            return await _context.PersonGrid.FromSqlRaw("EXEC spUserGrid").ToListAsync();
        }

        [HttpGet("programs")]
        public async Task<ActionResult<IEnumerable<RiseProgramGrid>>> GetPrograms()
        {
            return await _context.ProgramGrid.FromSqlRaw("EXEC spProgramGrid").ToListAsync();
        }

        [HttpGet("schools")]
        public async Task<ActionResult<IEnumerable<SchoolGrid>>> GetSchools()
        {
            return await _context.SchoolGrid.FromSqlRaw("EXEC spSchoolGrid").ToListAsync();
        }
    }
}