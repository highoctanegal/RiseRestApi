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
    public class RatingController : BaseApiController
    {
        public RatingController(RiseContext context) : base(context) { }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Rating>>> GetRating()
        {
            return await _context.Rating.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IModel>> GetRating(int id)
        {
            return await Get(id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutRating(int id, Rating rating)
        {
            return await Put(id, rating);
        }

        [HttpPost]
        public async Task<ActionResult<IModel>> PostRating(Rating rating)
        {
            return await Post(rating);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<IModel>> DeleteRating(int id)
        {
            return await Delete(id);
        }

        public override bool Exists(int id)
        {
            return _context.Rating.Any(e => e.RatingId == id);
        }

        public override async Task<IModel> FindAsync(int id)
        {
            return await _context.Rating.FindAsync(id) as IModel;
        }
    }
}
