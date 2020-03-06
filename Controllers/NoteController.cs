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

        [HttpGet("assessment/{assessmentId}")]
        public async Task<ActionResult<IEnumerable<NoteDetail>>> GetNotesByAssessment(int assessmentId)
        {
            return await _context.NoteDetail.FromSqlRaw("EXEC spNoteDetailByAssessment {0}", assessmentId).ToListAsync();
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

        [HttpPost("assessment/{assessmentId}")]
        public async Task<ActionResult<Note>> PostNoteToAssessment(int assessmentId, Note note)
        {
            var assessment = _context.Assessment.FirstOrDefault(a => a.AssessmentId == assessmentId);
            if (assessment == null)
            {
                return new BadRequestResult();
            }

            if (assessment.NoteId.HasValue)
            {
                var prevNote = _context.Note.FirstOrDefault(n => n.NoteId == assessment.NoteId);
                prevNote.NoteText = note.NoteText;
                prevNote.NoteDate = note.NoteDate;
                prevNote.AuthorPersonId = note.AuthorPersonId;
            }
            else
            {
                await Post(note);
                assessment.NoteId = note.NoteId;
            }
            await _context.SaveChangesAsync();

            return note;
        }

        [HttpPost("question/{str}")]
        public async Task<ActionResult<Note>> PostNoteToQuestion(string str, Note note)
        {
            var ids = str.Split(',');
            int questionId, assessmentId;
            if (ids.Length > 1)
            {
                int.TryParse(ids[0], out questionId);
                int.TryParse(ids[1], out assessmentId);
            }
            else
            {
                return new BadRequestResult();
            }

            var response = _context.AssessmentResponse.FirstOrDefault(a => a.AssessmentId == assessmentId && a.Rating.QuestionId == questionId);
            if (response == null)
            {
                return new BadRequestResult();
            }

            if (response.NoteId.HasValue)
            {
                var prevNote = _context.Note.FirstOrDefault(n => n.NoteId == response.NoteId);
                prevNote.NoteText = note.NoteText;
                prevNote.NoteDate = note.NoteDate;
                prevNote.AuthorPersonId = note.AuthorPersonId;
            }
            else
            {
                await Post(note);
                response.NoteId = note.NoteId;
            }
            await _context.SaveChangesAsync();

            return note;
        }

        [HttpPost("response/{responseId}")]
        public async Task<ActionResult<Note>> PostNoteToResponse(int responseId, Note note)
        {
            var response = _context.AssessmentResponse.FirstOrDefault(a => a.AssessmentResponseId == responseId);
            if (response == null)
            {
                return new BadRequestResult();
            }

            if (response.NoteId.HasValue)
            {
                var prevNote = _context.Note.FirstOrDefault(n => n.NoteId == response.NoteId);
                prevNote.NoteText = note.NoteText;
                prevNote.NoteDate = note.NoteDate;
                prevNote.AuthorPersonId = note.AuthorPersonId;
            }
            else
            {
                await Post(note);
                response.NoteId = note.NoteId;
            }
            await _context.SaveChangesAsync();

            return note;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Note>> DeleteNote(int id)
        {
            var assessment = _context.Assessment.FirstOrDefault(a => a.NoteId == id);
            if (assessment == null)
            {
                var response = _context.AssessmentResponse.FirstOrDefault(r => r.NoteId == id);
                if (response != null)
                {
                    response.NoteId = null;
                    _context.SaveChanges();
                }
            }
            else
            {
                assessment.NoteId = null;
                _context.SaveChanges();
            }


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
