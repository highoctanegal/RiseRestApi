using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RiseRestApi.Repository;
using System.Threading.Tasks;
using RiseRestApi.Models;

namespace RiseRestApi.Controllers
{
    public abstract class BaseApiController<U> : ControllerBase
        where U: IModel
    {
        protected readonly RiseContext _context;

        public BaseApiController(RiseContext context)
        {
            _context = context;
        }

        protected async Task<ActionResult<U>> Get(int id)
        {
            var model = await FindAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            return new JsonResult(model);
        }

        protected async Task<ActionResult<int>> Put(int id, U model)
        {
            _context.Entry(model).State = EntityState.Modified;

            try
            {
                return await _context.SaveChangesAsync();
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
        }

        protected async Task<ActionResult<int>> Post(U model)
        {
            _context.Add(model);
            await _context.SaveChangesAsync();
            return model.Id;
        }

        protected async Task<ActionResult<int>> Delete(int id)
        {
            var model = await FindAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            _context.Remove(model);
            return await _context.SaveChangesAsync();
        }

        protected abstract bool Exists(int id);

        protected abstract Task<U> FindAsync(int id);

    }
}