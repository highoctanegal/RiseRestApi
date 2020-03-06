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
    public class OrganizationController : BaseApiController<Organization>
    {
        public OrganizationController(RiseContext context) : base(context) { }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Organization>>> GetOrganization()
        {
            return await _context.Organization.Where(o => !o.IsRemoved).OrderBy(o => o.OrganizationName).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Organization>> GetOrganization(int id)
        {
            return await Get(id);
        }

        [HttpGet("{id}/detail")]
        public async Task<ActionResult<OrganizationDetail>> GetOrganizationDetail(int id)
        {
            if (!Exists(id))
            {
                return NotFound();
            }

            var model = await _context.OrganizationDetail.FromSqlRaw("EXEC dbo.spOrganizationDetail {0}", id).ToListAsync();

            if (model == null)
            {
                return NotFound();
            }

            return model.First();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrganization(int id, Organization person)
        {
            return await Put(id, person);
        }

        [HttpPost]
        public async Task<ActionResult<Organization>> PostOrganization(Organization person)
        {
            return await Post(person);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Organization>> DeleteOrganization(int id)
        {
            var organization = _context.Organization.Where(o => o.OrganizationId == id).FirstOrDefault();
            if (organization == null)
            {
                return new NotFoundResult();
            }
            organization.IsRemoved = true;
            await Put(id, organization);
            return new JsonResult(organization);
        }

        protected override bool Exists(int id)
        {
            return _context.Organization.Any(e => e.OrganizationId == id);
        }

        protected override async Task<Organization> FindAsync(int id)
        {
            return await _context.Organization.FindAsync(id) as Organization;
        }

    }
}