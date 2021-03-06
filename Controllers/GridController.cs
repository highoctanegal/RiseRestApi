﻿using Microsoft.AspNetCore.Mvc;
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

        [HttpGet("persons")]
        public async Task<ActionResult<IEnumerable<PersonGrid>>> GetPersons()
        {
            return await _context.PersonGrid.FromSqlRaw("EXEC spPersonGrid").ToListAsync();
        }

        [HttpGet("personsbycoach/{coachPersonId}")]
        public async Task<ActionResult<IEnumerable<PersonGrid>>> GetPersonsByCoach(int coachPersonId)
        {
            return await _context.PersonGrid.FromSqlRaw("EXEC spPersonGridByCoach {0}", coachPersonId).ToListAsync();
        }

        [HttpGet("personsbyorganization/{OrganizationId}")]
        public async Task<ActionResult<IEnumerable<PersonGrid>>> GetPersonsByOrganization(int organizationId)
        {
            return await _context.PersonGrid.FromSqlRaw("EXEC spPersonGridByOrganization {0}", organizationId).ToListAsync();
        }

        [HttpGet("personsbyprogram/{programId}")]
        public async Task<ActionResult<IEnumerable<PersonGrid>>> GetPersonsByProgram(int programId)
        {
            return await _context.PersonGrid.FromSqlRaw("EXEC spPersonGridByProgram {0}", programId).ToListAsync();
        }

        [HttpGet("programs")]
        public async Task<ActionResult<IEnumerable<RiseProgramGrid>>> GetPrograms()
        {
            return await _context.ProgramGrid.FromSqlRaw("EXEC spProgramGrid").ToListAsync();
        }

        [HttpGet("organizations")]
        public async Task<ActionResult<IEnumerable<OrganizationGrid>>> GetOrganizations()
        {
            return await _context.OrganizationGrid.FromSqlRaw("EXEC spOrganizationGrid").ToListAsync();
        }
    }
}