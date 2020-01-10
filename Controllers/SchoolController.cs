using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RiseRestApi.Models;
using RiseRestApi.Repository;

namespace RiseRestApi.Controllers
{
    [EnableCors("AllowAll")]
    [Route("api/[controller]")]
    [ApiController]
    public class SchoolController : BaseApiController
    {
        public SchoolController(RiseContext context) : base(context) { }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<School>>> GetSchool()
        {
            return await _context.School.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IModel>> GetSchool(int id)
        {
            return await Get(id);
        }

        [HttpGet("detail/{id}")]
        public async Task<ActionResult<SchoolDetail>> GetSchoolDetail(int id)
        {
            if (!Exists(id))
            {
                return NotFound();
            }

            var model = await _context.SchoolDetail.FromSqlRaw($"EXEC dbo.spSchoolDetail {id}").ToListAsync();

            if (model == null)
            {
                return NotFound();
            }

            return model.First();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutSchool(int id, School person)
        {
            return await Put(id, person);
        }

        [HttpPost]
        public async Task<ActionResult<IModel>> PostSchool(School person)
        {
            return await Post(person);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<IModel>> DeleteSchool(int id)
        {
            return await Delete(id);
        }

        public override bool Exists(int id)
        {
            return _context.School.Any(e => e.SchoolId == id);
        }

        public override async Task<IModel> FindAsync(int id)
        {
            return await _context.School.FindAsync(id) as IModel;
        }

    }
}