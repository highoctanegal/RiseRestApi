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
    public class AddressController : BaseApiController
    {
        public AddressController(RiseContext context) : base(context) { }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Address>>> GetAddress()
        {
            return await _context.Address.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IModel>> GetAddress(int id)
        {
            return await Get(id);
        }

        [HttpGet("person/{id}")]
        public async Task<ActionResult<IModel>> GetPersonAddress(int id)
        {
            var person = await _context.Person.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }
            var model = await _context.Address.FindAsync(person.AddressId) as IModel;

            if (model == null)
            {
                return NotFound();
            }

            return new JsonResult(model);
        }

        [HttpGet("school/{id}")]
        public async Task<ActionResult<IModel>> GetSchoolAddress(int id)
        {
            var school = await _context.School.FindAsync(id);
            if (school == null)
            {
                return NotFound();
            }
            var model = await _context.Address.FindAsync(school.AddressId) as IModel;

            if (model == null)
            {
                return NotFound();
            }

            return new JsonResult(model);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAddress(int id, Address assessment)
        {
            return await Put(id, assessment);
        }

        [HttpPost]
        public async Task<ActionResult<IModel>> PostAddress(Address assessment)
        {
            return await Post(assessment);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<IModel>> DeleteAddress(int id)
        {
            return await Delete(id);

        }

        public override bool Exists(int id)
        {
            return _context.Address.Any(e => e.AddressId == id);
        }

        public override async Task<IModel> FindAsync(int id)
        {
            return await _context.Address.FindAsync(id) as IModel;
        }
    }
}