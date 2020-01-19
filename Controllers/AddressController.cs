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
    public class AddressController : BaseApiController<Address>
    {
        public AddressController(RiseContext context) : base(context) { }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Address>>> GetAddress()
        {
            return await _context.Address.Where(a => !a.IsRemoved).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Address>> GetAddress(int id)
        {
            return await Get(id);
        }

        [HttpGet("person/{id}")]
        public async Task<ActionResult<Address>> GetPersonAddress(int id)
        {
            var person = await _context.Person.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }
            var model = await _context.Address.FindAsync(person.AddressId) as Address;

            if (model == null)
            {
                return NotFound();
            }

            return new JsonResult(model);
        }

        [HttpGet("Organization/{id}")]
        public async Task<ActionResult<Address>> GetOrganizationAddress(int id)
        {
            var Organization = await _context.Organization.FindAsync(id);
            if (Organization == null)
            {
                return NotFound();
            }
            var model = await _context.Address.FindAsync(Organization.AddressId) as Address;

            if (model == null)
            {
                return NotFound();
            }

            return new JsonResult(model);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAddress(int id, Address address)
        {
            return await Put(id, address);
        }

        [HttpPost]
        public async Task<ActionResult<Address>> PostAddress(Address address)
        {
            return await Post(address);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Address>> DeleteAddress(int id)
        {
            var address = _context.Address.Where(a => a.AddressId == id).FirstOrDefault();
            if (address == null)
            {
                return new NotFoundResult();
            }
            address.IsRemoved = true;
            await Put(id, address);
            return new JsonResult(address);
        }

        protected override bool Exists(int id)
        {
            return _context.Address.Any(e => e.AddressId == id);
        }

        protected override async Task<Address> FindAsync(int id)
        {
            return await _context.Address.FindAsync(id) as Address;
        }
    }
}