﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RiseRestApi.Models;
using RiseRestApi.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using System;

namespace RiseRestApi.Controllers
{
    [EnableCors("AllowAll")]
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : BaseApiController<Person>
    {
        public PersonController(RiseContext context) : base(context) { }

        [HttpGet("access/{id}")]
        public async Task<ActionResult<IEnumerable<AreaAccessDetail>>> GetAuthorization(int id)
        {
            var person = _context.Person.Where(p => p.PersonId == id && !p.IsRemoved).FirstOrDefault();
            
            if (person == null)
            {
                return NotFound();
            }

            return await _context.AreaAccessDetail.FromSqlRaw("EXEC dbo.spAreaAccessByPerson {0}", person.PersonId).ToListAsync();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Person>>> GetPerson()
        {
            return await _context.Person.Where(p => !p.IsRemoved).ToListAsync();
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
                .Where(p => p.RoleId == coachRole.RoleId).ToListAsync();
        }

        [HttpGet("organization/{organizationId}")]
        public async Task<ActionResult<IEnumerable<Person>>> GetPersonByOrganization(int organizationId)
        {
            return await _context.Person.Where(p => p.OrganizationId == organizationId).ToListAsync();
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

        [HttpGet("login/{email}")]
        public async Task<ActionResult<PersonDetail>> Login(string email)
        {
            var person = _context.Person.Where(p => p.Email == email && !p.IsRemoved).FirstOrDefault();
            if (person == null)
            {
                return NotFound();
            }
            person.LastLogin = DateTime.UtcNow;
            _context.SaveChanges();
            
            var model = await _context.PersonDetail.FromSqlRaw("EXEC dbo.spPersonDetail {0}", person.PersonId)
                .ToListAsync();
            return model.First();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPerson(int id, Person person)
        {
            return await Put(id, person);
        }

        [HttpPost]
        public async Task<ActionResult<Person>> PostPerson(Person person)
        {
            return await Post(person);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Person>> DeletePerson(int id)
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
    }
}
