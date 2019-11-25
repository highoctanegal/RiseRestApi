using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RiseRestApi.Models;
using RiseRestApi.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RiseRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : BaseApiController
    {
        public NoteController(RiseContext context) : base(context) { }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Note>>> GetNotes()
        {
            return await _context.Note.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IModel>> GetNote(int id)
        {
            return await Get(id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutNote(int id, Note note)
        {
            return await Put(id, note);
        }

        [HttpPost]
        public async Task<ActionResult<IModel>> PostNote(Note note)
        {
            return await Post(note);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<IModel>> DeleteNote(int id)
        {
            return await Delete(id);
        }

        public override bool Exists(int id)
        {
            return _context.Note.Any(e => e.NoteId == id);
        }

        public override async Task<IModel> FindAsync(int id)
        {
            return await _context.Note.FindAsync(id) as IModel;
        }
    }
}
