using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RiseRestApi.Models;
using RiseRestApi.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RiseRestApi.Controllers
{
    [EnableCors("AllowAll")]
    [Route("api/[controller]")]
    [ApiController]
    public class SystemController : ControllerBase
    {
        protected readonly RiseContext _context;

        public SystemController(RiseContext context)
        {
            _context = context;
        }

        [HttpGet("roles")]
        public async Task<ActionResult<IEnumerable<Role>>> GetRoles()
        {
            return await _context.Role.ToListAsync();
        }
    }
}