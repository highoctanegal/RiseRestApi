using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RiseRestApi.Repository;
using RiseRestApi.Models;
using System.Threading.Tasks;

namespace RiseRestApi.Controllers
{
    public abstract class BaseApiController : ControllerBase
    {
        protected readonly RiseContext _context;

        public BaseApiController(RiseContext context)
        {
            _context = context;
        }

        public async Task<ActionResult<IModel>> Get(int id)
        {
            var model = await FindAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            return new JsonResult(model);
        }

        public async Task<IActionResult> Put(int id, IModel model)
        {
            _context.Entry(model).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Exists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        public async Task<ActionResult<IModel>> Post(IModel model)
        {
            _context.Add(model);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAssessment", new { model.Id }, model);
        }

        public async Task<ActionResult<IModel>> Delete(int id)
        {
            var model = await FindAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            _context.Remove(model);
            await _context.SaveChangesAsync();

            return new JsonResult(model);
        }

        public abstract bool Exists(int id);

        public abstract Task<IModel> FindAsync(int id);

    }
}