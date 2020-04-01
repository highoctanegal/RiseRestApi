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

        [HttpGet("map")]
        public async Task<ActionResult<IDictionary<int,IEnumerable<Rating>>>> GetRatingByQuestion()
        {
            var ratings = await _context.Rating.ToListAsync();
            var ratingsMap = new Dictionary<int, IList<Rating>>();
            foreach(var rating in ratings.OrderBy(r => r.QuestionId).ThenBy(r => r.Score))
            {
                List<Rating> ratingList;
                if (ratingsMap.ContainsKey(rating.QuestionId))
                {
                    ratingList = (List<Rating>)ratingsMap[rating.QuestionId];
                } else {
                    ratingList = new List<Rating>();
                    ratingsMap.Add(rating.QuestionId, ratingList);
                }
                ratingList.Add(rating);
            }
            return new JsonResult(ratingsMap);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Rating>> GetRating(int id)
        {
            return await Get(id);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<int>> PutRating(int id, Rating rating)
        {
            return await Put(id, rating);
        }

        [HttpPost]
        public async Task<ActionResult<int>> PostRating(Rating rating)
        {
            return await Post(rating);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> DeleteRating(int id)
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
