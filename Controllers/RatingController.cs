using Microsoft.AspNetCore.Mvc;
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
    public class RatingController : BaseApiController<Rating>
    {
        public RatingController(RiseContext context) : base(context) { }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Rating>>> GetRating()
        {
            return await _context.Rating.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Rating>> GetRating(int id)
        {
            return await Get(id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutRating(int id, Rating rating)
        {
            return await Put(id, rating);
        }

        [HttpPost]
        public async Task<ActionResult<Rating>> PostRating(Rating rating)
        {
            return await Post(rating);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Rating>> DeleteRating(int id)
        {
            return await Delete(id);
        }
        protected override bool Exists(int id)
        {
            return _context.Rating.Any(e => e.RatingId == id);
        }

        protected override async Task<Rating> FindAsync(int id)
        {
            return await _context.Rating.FindAsync(id) as Rating;
        }
    }
}
