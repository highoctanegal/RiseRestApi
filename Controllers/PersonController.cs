using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RiseRestApi.Models;
using RiseRestApi.Repository;
using RiseRestApi.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RiseRestApi.Controllers
{
    [EnableCors("AllowAll")]
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : BaseApiController<Person>
    {
        public PersonController(RiseContext context) : base(context) { }

        [HttpGet("authorization/{id}")]
        public async Task<ActionResult<IEnumerable<AreaAuthorizationDetail>>> GetAuthorization(int id)
        {
            var person = _context.Person.Where(p => p.PersonId == id && !p.IsRemoved).FirstOrDefault();

            if (person == null)
            {
                return NotFound();
            }

            return await _context.AreaAuthorizationDetail.FromSqlRaw("EXEC dbo.spAreaAuthorizationByPerson {0}", person.PersonId).ToListAsync();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Person>>> GetPerson()
        {
            return await _context.Person.Where(p => !p.IsRemoved).OrderBy(o => o.LastName).ToListAsync();
        }

        [HttpGet("role/{roleName}")]
        public async Task<ActionResult<IEnumerable<Person>>> GetPersonByRole(string roleName)
        {
            if (string.IsNullOrEmpty(roleName))
            {
                return NotFound();
            }

            var coachRole = _context.Role.ToList().FirstOrDefault(r => r.RoleName.ToLower() == roleName.ToLower());

            return await _context.Person
                .Where(p => p.RoleId == coachRole.RoleId).OrderBy(p => p.LastName).ToListAsync();
        }

        [HttpGet("organization/{organizationId}")]
        public async Task<ActionResult<IEnumerable<Person>>> GetPersonByOrganization(int organizationId)
        {
            return await _context.Person.Where(p => p.OrganizationId == organizationId)
                .OrderBy(p => p.LastName).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Person>> GetPerson(int id)
        {
            return await Get(id);
        }

        [HttpGet("{id}/detail")]
        public async Task<ActionResult<PersonDetail>> GetPersonDetail(int id)
        {
            if (!Exists(id))
            {
                return NotFound();
            }

            var model = await _context.PersonDetail.FromSqlRaw("EXEC dbo.spPersonDetail {0}", id)
                .ToListAsync();
            return model.First();
        }

        [HttpGet("checkemail/{email}")]
        public async Task<ActionResult<bool>> CheckEmail(string email)
        {
            return await _context.Person.AnyAsync(p => p.Email == email);
        }

        [HttpGet("login/{email}")]
        public async Task<ActionResult<PersonDetail>> Login(string email)
        {
            var person = _context.Person.Where(p => p.Email == email && !p.IsRemoved).FirstOrDefault();
            if (person == null)
            {
                var emailer = new Emailer();
                await emailer.SendEmailToAdminUnauthorizedLogin(email);
                return Unauthorized();
            }
            person.LastLogin = DateTime.UtcNow;
            _context.SaveChanges();

            var model = await _context.PersonDetail.FromSqlRaw("EXEC dbo.spPersonDetail {0}", person.PersonId)
                .ToListAsync();
            return model.First();
        }

        [HttpGet("login/{email}/{firstName}/{lastName}/{programCode}")]
        public async Task<ActionResult<PersonDetail>> LoginNew(string email, string firstName, string lastName,
            string programCode)
        {
            RiseProgram program;
            if ((program = GetProgramFromCode(programCode)) == null)
            {
                return Unauthorized();
            }
            var person = _context.Person.FirstOrDefault(p => p.Email == email);
            if (person != null)
            {
                if (person.IsRemoved)
                {
                    return Unauthorized();
                }
                person.LastLogin = DateTime.UtcNow;
                await _context.SaveChangesAsync();
            }
            else
            {
                AddNewPerson(email, firstName, lastName, program);
            }

            var model = await _context.PersonDetail.FromSqlRaw("EXEC dbo.spPersonDetail {0}", person.PersonId)
                .ToListAsync();
            var emailer = new Emailer();
            await emailer.SendEmailToAdminNewUser(programCode, program, model.First());

            return model.First();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<int>> PutPerson(int id, Person person)
        {
            return await Put(id, person);
        }

        [HttpPost]
        public async Task<ActionResult<int>> PostPerson(Person person)
        {
            return await Post(person);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> DeletePerson(int id)
        {
            var person = _context.Person.Where(p => p.PersonId == id).FirstOrDefault();
            if (person == null)
            {
                return new NotFoundResult();
            }
            person.IsRemoved = true;
            await Put(id, person);
            return new JsonResult(person);
        }

        protected override bool Exists(int id)
        {
            return _context.Person.Any(e => e.PersonId == id);
        }

        protected override async Task<Person> FindAsync(int id)
        {
            return await _context.Person.FindAsync(id) as Person;
        }

        protected RiseProgram GetProgramFromCode(string programCode)
        {
            if (string.IsNullOrEmpty(programCode))
            {
                return null;
            }
            var codes = programCode.Split('-');
            if (codes.Length < 2)
            {
                return null;
            }
            var organization = _context.Organization.FirstOrDefault(o => o.Code == codes[0]);
            var program = _context.Program.FirstOrDefault(p => p.Code == codes[1]);
            if (organization == null || program == null || program.OrganizationId != organization.OrganizationId)
            {
                return null;
            }
            return program;
        }

        protected async void AddNewPerson(string email, string firstName, string lastName,
            RiseProgram program)
        {
            var role = _context.Role.FirstOrDefault(r => r.RoleName == "Entrepreneur");
            var person = new Person
            {
                Email = email,
                FirstName = firstName,
                LastName = lastName,
                RoleId = role.RoleId,
                ProgramId = program.ProgramId,
                OrganizationId = program.OrganizationId,
                IsRemoved = false,
                LastLogin = DateTime.UtcNow
            };
            await _context.Person.AddAsync(person);
            await _context.SaveChangesAsync();
        }

        
    }
}
