﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RiseRestApi.Models;
using RiseRestApi.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;

namespace RiseRestApi.Controllers
{
    [EnableCors("AllowAll")]
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : BaseApiController
    {
        public PersonController(RiseContext context) : base(context) { }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Person>>> GetPerson()
        {
            return await _context.Person.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IModel>> GetPerson(int id)
        {
            return await Get(id);
        }

        [HttpGet("detail/{id}")]
        public async Task<ActionResult<PersonDetail>> GetPersonDetail(int id)
        {
            if (!Exists(id))
            {
                return NotFound();
            }

            var model = await _context.PersonDetail.FromSqlRaw($"EXEC dbo.spPersonDetail {id}").ToListAsync();

            if (model == null)
            {
                return NotFound();
            }

            return model.First();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPerson(int id, Person person)
        {
            return await Put(id, person);
        }

        [HttpPost]
        public async Task<ActionResult<IModel>> PostPerson(Person person)
        {
            return await Post(person);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<IModel>> DeletePerson(int id)
        {
            return await Delete(id);
        }

        public override bool Exists(int id)
        {
            return _context.Person.Any(e => e.PersonId == id);
        }

        public override async Task<IModel> FindAsync(int id)
        {
            return await _context.Person.FindAsync(id) as IModel;
        }

    }
}
