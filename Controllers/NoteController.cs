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
    public class NoteController : BaseApiController<Note>
    {
        public NoteController(RiseContext context) : base(context) { }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Note>>> GetNotes()
        {
            return await _context.Note.ToListAsync();
        }

        [HttpGet("person/{personId}")]
        public async Task<ActionResult<IEnumerable<NoteDetail>>> GetNotesByPerson(int personId)
        {
            return await _context.NoteDetail.FromSqlRaw("EXEC spNoteDetailByPerson {0}", personId).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Note>> GetNote(int id)
        {
            return await Get(id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutNote(int id, Note note)
        {
            return await Put(id, note);
        }

        [HttpPost]
        public async Task<ActionResult<Note>> PostNote(Note note)
        {
            return await Post(note);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Note>> DeleteNote(int id)
        {
            return await Delete(id);
        }

        protected override bool Exists(int id)
        {
            return _context.Note.Any(e => e.NoteId == id);
        }

        protected override async Task<Note> FindAsync(int id)
        {
            return await _context.Note.FindAsync(id) as Note;
        }
    }
}
